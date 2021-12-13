using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace BehaviourTreeAI {
    public class BehaviourTreeRunner : MonoBehaviour {

        // The main behaviour tree asset
        public BehaviourTree tree;

        // Storage container object to hold game object subsystems
        Context context;

        // Start is called before the first frame update
        void Start() {
            context = CreateBehaviourTreeContext();
            tree = tree.Clone();
            tree.Bind(context);
        }

        // Update is called once per frame
        void Update() {
            if (tree && EnemiesManager.Instance.photonView.IsMine) {
                Debug.Log("I'm the master");
            tree.Update();
            }
            
        }

        Context CreateBehaviourTreeContext() {
            return Context.CreateFromGameObject(gameObject);
        }

        private void OnDrawGizmosSelected() {
            if (!tree) {
                return;
            }

            BehaviourTree.Traverse(tree.rootNode, (n) => {
                if (n.drawGizmos) {
                    n.OnDrawGizmos();
                }
            });
        }

        public Context GetContext()
        {
            return context;
        }
    }
}