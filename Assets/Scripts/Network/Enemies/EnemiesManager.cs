using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviourPunCallbacks
{
    public static EnemiesManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private int lastId = 0;
    [SerializeField] private GameObject enemy;
    


    // Instantiate an enemy with RPC.
    // How to call it : 
    // EnemiesManager.Instance.photonView.RPC("InstantiateEnemy", RpcTarget.All, position);
    [PunRPC]
    public void InstantiateEnemy(Vector3 position)
    {
        GameObject enemyInstantiate = Instantiate(enemy, position, Quaternion.identity);
        enemyInstantiate.transform.parent = gameObject.transform;
        enemyInstantiate.GetComponent<EnemyScript>().ID = lastId;
        lastId++;
    }


    // Parcours tous les enfants de l'EnemiesManager en fonction d'un ID.
    // Appel par la suite la fonction TakeDamage de l'enfant en question
    // Pour appeler la fonction :
    // this.photonView.RPC("EnemyTakeDamageWithId", RpcTarget.All, id, damages);
    [PunRPC]
    public void EnemyTakeDamageWithId(int id, float damages)
    {
        foreach (Transform child in transform)
        {
            EnemyScript enemy = child.GetComponent<EnemyScript>();
            if (enemy.ID == id)
            {
                enemy.TakeDamage(damages);
                return;
            }
        }
    }

    [PunRPC]
    public void EnemyDieWithId(int id)
    {
        foreach (Transform child in transform)
        {
            EnemyScript enemy = child.GetComponent<EnemyScript>();
            if (enemy.ID == id)
            {
                enemy.Die();
                return;
            }
        }
    }

    [PunRPC]
    public void EnemyGetHit(int id)
    {
        foreach (Transform child in transform)
        {
            EnemyScript enemy = child.GetComponent<EnemyScript>();
            if (enemy.ID == id)
            {
                enemy.GetHit();
                return;
            }
        }
    }

    [PunRPC]
    public void EnemyMoveToPositionWithId(int id, Vector3 position, float stoppingDistance, float speed, float angularSpeed, bool updateRotation, float acceleration)
    {
        foreach (Transform child in transform)
        {
            EnemyScript enemy = child.GetComponent<EnemyScript>();
            if (enemy.ID == id)
            {
                enemy.MoveToPosition(position, stoppingDistance, speed, angularSpeed, updateRotation, acceleration);
                return;
            }
        }
    }

    [PunRPC]
    public void EnemyBiteAttackWithId(int id)
    {
        foreach (Transform child in transform)
        {
            EnemyScript enemy = child.GetComponent<EnemyScript>();
            if (enemy.ID == id)
            {
                enemy.BiteAttack();
                return;
            }
        }
    }

    [PunRPC]
    public void EnemyClawsAttackWithId(int id, float attackAngle = 0f)
    {
        foreach (Transform child in transform)
        {
            EnemyScript enemy = child.GetComponent<EnemyScript>();
            if (enemy.ID == id)
            {
                enemy.ClawsAttack(attackAngle);
                return;
            }
        }
    }

    [PunRPC]
    public void EnemyJumpBiteAttackWithId(int id)
    {
        foreach (Transform child in transform)
        {
            EnemyScript enemy = child.GetComponent<EnemyScript>();
            if (enemy.ID == id)
            {
                enemy.JumpBiteAttack();
                return;
            }
        }
    }

    [PunRPC]
    public void EnemySpitAttackWithId(int id)
    {
        foreach (Transform child in transform)
        {
            EnemyScript enemy = child.GetComponent<EnemyScript>();
            if (enemy.ID == id)
            {
                enemy.SpitAttack();
                return;
            }
        }
    }

    [PunRPC]
    public void EnemyDisengageJumpWithId(int id, float jumpLengthX = 0f, float jumpLengthY = 0f)
    {
        foreach (Transform child in transform)
        {
            EnemyScript enemy = child.GetComponent<EnemyScript>();
            if (enemy.ID == id)
            {
                enemy.DisengageJump(jumpLengthX, jumpLengthY);
                return;
            }
        }
    }

    [PunRPC]
    public void EnemyDodgeWithId(int id)
    {
        foreach (Transform child in transform)
        {
            EnemyScript enemy = child.GetComponent<EnemyScript>();
            if (enemy.ID == id)
            {
                enemy.Dodge();
                return;
            }
        }
    }

    [PunRPC]
    public void EnemyRoarWithId(int id)
    {
        foreach (Transform child in transform)
        {
            EnemyScript enemy = child.GetComponent<EnemyScript>();
            if (enemy.ID == id)
            {
                enemy.Roar();
                return;
            }
        }
    }


}
