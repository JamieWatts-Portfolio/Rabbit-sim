using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using AI;
using utility;

namespace AI {  
    public class ExploreBehaviour : StateMachineBehaviour
    {
        #region Inspector
        [Tooltip("Parent IEntity script. It not set, it will be inherited it automatically from parent.")]
        [SerializeField] public IEntity parentEntity = null;

        public float minInvestigateTime = 3;
        public float maxInvestigateTime = 10;

        public byte investigateRadius = 2;

        public List<string> investagatableTags = new List<string>();

        // Tollerance for declaring at destination when moving to random positions.
        public float randomMovementDistanceTollerance = 0.2f;
        public float pathingTimeout = 20f;  // After 20s,


        #endregion

        #region private
        private Animator animator = null;
        private GameObject objectOfInterest = null;
        private bool isInvestigating = false;
        private bool isAtInvestigationArea = false;

        // time spend investigating area of interest
        private float areaInvestigationDelta = 0f;
        private float totalInvestigationDelta = 0f;


        private float investigationSatificationTime = 0f;  // current random time between min and max investigation time. Declares how long we'll actually stay to investigate the area.
        #endregion

        #region Statemachine interface
        override public void OnStateEnter(Animator Animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator = Animator;
            parentEntity = IEntity.getAnimatorIEntity(animator);                                                                // If no IEntity was set, try to inherit it from the parent entity.
            BeginInvestigating();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => UpdateInvestigation();
        #endregion

        #region investigation behaviour
        /// <summary> Chooses and begins investigating something by random weight </summary>
        private void BeginInvestigating(){
                switch (Random.Range(0,2)) {
                case 0:                  // Return to parent
                    Investigate(parentEntity.gameObject.transform.parent.gameObject);
                    break;

                case 1:                  // Investigate random point
                    // Investigate(tools.randomInTransform(GameObject.Find(Literals.OBJECT_WORLD).transform, true)); //TODO change ignorey to raycast
                    break;

                case 2:                   // Investigate random object
                    Investigate(GameObject.Find(tools.RandomInList<string>(investagatableTags)));
                    break;
                case 3:                  // TODO raycast item in front of rabbit
                    break;
            }
        }

        /// <summary> Starts investigative behaviour surrounding parsed object. </summary>
        /// Overrides exsisting objects of interest.
        /// Sets objectOfInterrest, sets destintaion, resets deltas
        private void Investigate(GameObject toInvestigate){
            isInvestigating = true;                                                                                                 // Raise investigating flag.
            isAtInvestigationArea = false;
            objectOfInterest = toInvestigate;                                                                                       // Store object of interest locally
            parentEntity.navigation.SetDestination(objectOfInterest.transform.position);                                            // Set entity's destination
            if (parentEntity.navigation.pathStatus == NavMeshPathStatus.PathInvalid) CompleteInvestigation();                       // If pathing is not valid, skip
            investigationSatificationTime = Random.Range(minInvestigateTime, maxInvestigateTime);                                   // Choose how long we'll investigate the AOI
            ResetDeltas();                                                                                                          // Clear exsisting explore delta times
        }

        private void UpdateInvestigation() {
            /*
                NOT AT AREA OF INTEREST
            */
            if (!isInvestigating) return;                                                                                           // Reject update call if we're not investigating.  
            if (!isAtInvestigationArea) 
            {
                isAtInvestigationArea = parentEntity.navigation.remainingDistance <= investigateRadius;                             // If within investigation area, raise at area flag.                     
                if (totalInvestigationDelta > pathingTimeout) CompleteInvestigation();                                              // Pathing timed out, exit investigation.
                return;                                                                                                             // Can't do investigation behaviours untill pathing to LOI is complete.
            }

            /*
                AT AREA OF INTEREST
            */
            if (areaInvestigationDelta > investigationSatificationTime) CompleteInvestigation();                                     // If we've been at the AOI for long enough to satisfy the entity, complete the investigation.

            if (parentEntity.navigation.remainingDistance < randomMovementDistanceTollerance || parentEntity.navigation.remainingDistance == Mathf.Infinity)
                parentEntity.MoveRandom(investigateRadius);                                                                          // If at destination, or no there is no desination, set a random one.

        }

        /// <summary>Clears investigation data, prevents further updates, invokes idle state</summary>
        private void CompleteInvestigation() {
            ResetDeltas();                                    // Clear investigation data
            isAtInvestigationArea = false;                  
            isInvestigating = false;                          // Clear investigating flag to stop any update attempts
            animator.SetTrigger(Literals.ST_TRIG_IDLE_RETURN);// Invoke statemachine to return to idle state.
        }

        #endregion

        #region Utility
        private void ResetDeltas() {
            areaInvestigationDelta = 0f;
            totalInvestigationDelta = 0f;
        }

        private void UpdateDeltas(){
            totalInvestigationDelta += Time.deltaTime;
            areaInvestigationDelta += Time.deltaTime;
        }
        #endregion
     }
}

            // if (parentEntity.navigation.pathStatus == NavMeshPathStatus.PathComplete) {                                         // If previous navigation status is complete 
            //     if (UnityEngine.Random.Range(0,10) > 5){                                                                        // Random weight,
            //         parentEntity.navigation.SetDestination(GameObject.Find("Rabbit Burrow").transform.position);                // Go to a burrow
            //     } else {
            //         parentEntity.MoveRandom(10);                                                                                // Got to a random position
            //     }
            // } 