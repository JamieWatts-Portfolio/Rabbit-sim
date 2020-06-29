using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using AI;
using utility;

namespace AI
{
    /// <summary>Exploritory entity statemachine component</summary>
    /// Curiously explores an area of interest from different angles.
    /// Areas of interest may be random objects, specific tags, parent object, or a random vector.
    /// Once finished exploring one area, state exits.
    /// Failiure to start or complete exploring area also invokes state exit.
    public class ExploreBehaviour : StateMachineBehaviour
    {
        #region Inspector
        [Header("Exploritory behaviour")]
        /// <summary>IEntity which this behaviour controlls.</summary>
        /// When not set, it will be attempted to be inherited using the animator monocomponent upon state enter.
        [Tooltip("Parent IEntity script. It not set, it will be inherited it automatically from parent.")]
        [SerializeField] public IEntity parentEntity = null;

        [Tooltip("Radius exponent; declares how large the area of interest is.")]
        public byte investigateRadius = 2;

        [Tooltip("GO tags that may be investigated. If tags are selected but none are set, state will automatically exit.")]
        public List<string> investagatableTags = new List<string>();
        
        [Tooltip("Tollerance for declaring self at destination when moving to random positions.")]
        public float randomMovementDistanceTollerance = 0.1f;

        [Header("Delta times")]
        [Tooltip("Minimum time to investigate the area of interest")]
        public float minInvestigateTime = 3;

        [Tooltip("Maximum time to investigate the area of interest")]
        public float maxInvestigateTime = 10;

        [Tooltip("Maximum time to wait in each position in area of interest, examining at point of interest.")]
        public float maxPositionTimeout = 3f;

        [Tooltip("State will exit if area of interest is not reached within this time.")]
        public float pathingTimeout = 5f; 
        #endregion

        #region private
        /// <summary> Local reference to the parenting animator for routinal usage </summary>
        private Animator animator = null;

        /// <summary> Transform data for the location of interest</summary>
        private TransformData loactionOfInterest = new TransformData();

        /// <summary>Declares if this behaviour is actively investigating.</summary>
        /// When updated whilst low, state will automatically exit.
        private bool isInvestigating = false;

        /// <summary> Determines if we've successfully pathed to the area of interest, and may begin exploring it.</summary>
        private bool isAtInvestigationArea = false;

        /// <summary> Determines if we're at a stationary position within the area of interest</summary>
        /// (Stopped, looking at the interest)
        private bool isAtInvestigationPosition = false;

        /// <summary> Flag used to trigger event upon first arriving at each stationary position</summary>
        private bool investigationPositionArrivalFlag = false;

        /// <summary> Time spent investigating area of interest (s)</summary>
        private float areaInvestigationDelta = 0f;

        /// <summary> Time spent at current stationary position. </summary>
        /// Used to timeout and move to next position in area.
        private float stationaryPositionDelta = 0f;

        /// <summary> Time since investigation began (s)</summary>
        private float totalInvestigationDelta = 0f;

        /// <summary> Declares how long we'll actually stay investigating after entering the AOI. </summary>
        public float investigationSatificationTime = 0f; 
        #endregion

        #region Statemachine interface
        override public void OnStateEnter(Animator Animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator = Animator;                                              // Store reference to parenting statemachine
            parentEntity = IEntity.getAnimatorIEntity(animator);              // If no IEntity was set, try to inherit it from the parent entity.
            BeginInvestigating();                                             // Attempt to begin an investigation
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => UpdateInvestigation();
        #endregion

        #region investigation behaviour
        /// <summary> Chooses and begins investigating something by random weight </summary>
        public void BeginInvestigating()
        {
            switch (Random.Range(0, 3))
            {
                case 0:                   // Investigate parent
                    Investigate(parentEntity.gameObject.transform.parent.gameObject);
                    break;

                case 1:                   // Investigate random point
                    GameObject world = GameObject.Find(Literals.OBJECT_WORLD);
                    Investigate(tools.PickRandomLocation(world.GetComponent<MeshCollider>(), world.transform, 20));
                    break;

                case 2:                   // Investigate random object
                    Investigate(GameObject.Find(tools.RandomInList<string>(investagatableTags)));
                    break;
                case 3:                  // Investigate raycast item in front of entity
                    Ray ray = new Ray(new Vector3(parentEntity.gameObject.transform.position.x, parentEntity.gameObject.transform.position.y, parentEntity.gameObject.transform.position.z), parentEntity.gameObject.transform.rotation.eulerAngles);                  // Create a new ray at the specified location, at height, facing towards the ground.
                    RaycastHit hit;
                    if (parentEntity.gameObject.GetComponent<MeshCollider>().Raycast(ray, out hit, 1000))
                        Investigate(hit.transform.gameObject);
                    break;
            }
        }

        /// <summary> Starts investigative behaviour surrounding parsed object. </summary>
        /// Overrides exsisting objects of interest.
        /// Sets areaOfInterrest and destintaion. resets deltas.
        public void Investigate(GameObject toInvestigate)
        {
            if (toInvestigate == null) {CompleteInvestigation(); return;}
            loactionOfInterest = TransformData.CloneTransform(toInvestigate.transform);
            Investigate(loactionOfInterest.position);
        }

        /// <summary> Starts investigative behaviour surrounding parsed vector. </summary>
        /// Overrides exsisting objects of interest.
        /// Sets areaOfInterrest and destintaion. resets deltas.
        public void Investigate(Vector3 vector)
        {
            if (vector == null) {CompleteInvestigation(); return;}                                               // If nothing to investigate, trigger exit and don't continue.

            // Configure flags
            isInvestigating = true;                                                                              
            isAtInvestigationArea = false;
            investigationPositionArrivalFlag = true;

            // Store object of interest locally 
            loactionOfInterest = new TransformData();                                                            
            loactionOfInterest.position = vector;

            parentEntity.navigation.SetDestination(loactionOfInterest.position);                                       // Set entity's destination
            if (parentEntity.navigation.pathStatus == NavMeshPathStatus.PathInvalid) {CompleteInvestigation(); return;}// If there's no path to destination, exit state.
            investigationSatificationTime = Random.Range(minInvestigateTime, maxInvestigateTime);                      // Choose how long we'll investigate the AOI
            ResetDeltas();                                                                                             // Reset any exsisting delta time data
        }



        public void UpdateInvestigation()
        {
            /*
                NOT AT AREA OF INTEREST
            */
            if (!isInvestigating) CompleteInvestigation();                                                                          // Reject update call if we're not investigating.
            UpdateDeltas();                                                                                                         // Update investigation deltas since last update.  
            if (!isAtInvestigationArea)
            {
                isAtInvestigationArea = parentEntity.navigation.remainingDistance <= investigateRadius;                             // If within investigation area, raise at area flag.                     
                if (totalInvestigationDelta > pathingTimeout) CompleteInvestigation();                                              // Pathing timed out, exit investigation.
                areaInvestigationDelta = 0f;
                return;                                                                                                             // Can't do investigation behaviours untill pathing to LOI is complete.
            }

            /*
                AT AREA OF INTEREST
            */
            if (areaInvestigationDelta > investigationSatificationTime) CompleteInvestigation();                                     // If we've been at the AOI for long enough to satisfy the entity, complete the investigation.
            
            if (parentEntity.navigation.remainingDistance < randomMovementDistanceTollerance || parentEntity.navigation.remainingDistance == Mathf.Infinity){ // If at investigation point,
                    if (investigationPositionArrivalFlag) AtNewInvestigationPosition();                                           // Invoke just arrived if arrival flag is high
                    if (isAtInvestigationPosition && stationaryPositionDelta < 0) EnterNewInvestigationPosition();                                                // If we've been at this position for long enough, move to a new one.
            }
        }

        private void EnterNewInvestigationPosition()
        {
            isAtInvestigationPosition = false;
            parentEntity.MoveRandom(investigateRadius);                                                                             // When ready go to new point to investigate.
            investigationPositionArrivalFlag = true;
        }

        private void AtNewInvestigationPosition(){
                isAtInvestigationPosition = true;
                investigationPositionArrivalFlag = false;
                stationaryPositionDelta = Random.Range(0, maxPositionTimeout);
                iTween.LookTo(parentEntity.gameObject, loactionOfInterest.position, 0.5f);                                            // Once at new position, look at it interest
        }

        /// <summary>Clears investigation data, prevents further updates, invokes idle state</summary>
        private void CompleteInvestigation()
        {
            ResetDeltas();                                    // Clear investigation data
            isAtInvestigationArea = false;
            isInvestigating = false;                          // Clear investigating flag to stop any update attempts
            animator.SetTrigger(Literals.ST_TRIG_IDLE_RETURN);// Invoke statemachine to return to idle state.
        }

        #endregion

        #region Utility
        public void ResetDeltas()
        {
            stationaryPositionDelta = 0f;
            areaInvestigationDelta = 0f;
            totalInvestigationDelta = 0f;
        }

        public void UpdateDeltas()
        {
            stationaryPositionDelta -= Time.deltaTime;
            totalInvestigationDelta += Time.deltaTime;
            areaInvestigationDelta += Time.deltaTime;
        }
        #endregion
    }
}