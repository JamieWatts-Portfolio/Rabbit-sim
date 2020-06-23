using UnityEngine;
using AI;

namespace AI{

    public class Move : MonoBehaviour {
        
        #region targets

        /// <summary>Target of controll; what this controlls.</summary>
        public UnityEngine.AI.NavMeshAgent controlTarget;

        /// <summary>Target of location</summary>
        public GameObject globalTarget;

        #endregion

        public Move(UnityEngine.AI.NavMeshAgent ControlTarget) => controlTarget = ControlTarget;

        public void Update(){
            if (globalTarget != null) {
                if (!controlTarget.destination.Equals(globalTarget.transform.position)) {
                    controlTarget.SetDestination(globalTarget.transform.position);                  //This is shit, should be on an event not an update
                }                                             
                        
            }
        }

        public void Start(){

        }
    }
}