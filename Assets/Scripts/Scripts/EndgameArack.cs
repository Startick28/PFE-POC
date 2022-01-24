using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EndgameArack : MonoBehaviour
{

    public Vector3 FinalObjective;

    private NavMeshAgent agent;
    private BehaviourTreeAI.BehaviourTreeRunner treeRunner;
    private Rigidbody rb;

    private bool touchFloor = false;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        treeRunner = GetComponent<BehaviourTreeAI.BehaviourTreeRunner>();
        rb = GetComponent<Rigidbody>();
        agent.enabled = false;
        treeRunner.enabled = false;
        rb.useGravity = true;
        rb.drag = 0;
    }


    void Update()
    {
        int layerMask = (1 << 10);
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, 1.5f, layerMask))
        {
            rb.useGravity = false;
            rb.drag = float.PositiveInfinity;
            agent.enabled = true;
            treeRunner.enabled = true;
            Destroy(this);
        }
    }
}
