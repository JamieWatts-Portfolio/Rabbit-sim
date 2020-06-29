using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using CodeMonkey.Utils;


namespace AI {

  
    /// <summary>Base entity value and interactions</summary>
    [RequireComponent(typeof(MeshCollider))]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Rigidbody))]
    public class IEntity : MonoBehaviour { 

        #region movement
        /// <summary>Inspector set nav mesh agent<summary>
        /// Basis for all ientity movement.
        [SerializeField] public NavMeshAgent navigation;

        /// <summary>Base movement speet</summary>
        public int baseSpeed {get; private set;} = 10;

        /// <summary>speed modifyer</summary>
        /// Multiplier for speed
        /// <c>speed = base speed * speed modifyer<c>
        public int speedMod {get; set;} = 10;

        /// <summary>Base stamina</summary>
        public int baseStamina;

        /// <summary>Distance for senory reception in entities</summary>
        /// Includes sight and smell
        public int senseDistance;

        /// <summary>sense modifyer</summary>
        /// Multiplier for sense
        /// <c>sence = base sense * sense modifyer<c>
        public int senseMod {get; set;}
        
        /// <summary>Moves the parent entity is a random direction</summary>
        /// Overrides any exsisting destination
        /// <param name="scale">Scale exponent. Random movement is scaled by this.</param>
        public void MoveRandom(byte scale) => MoveRelative(UtilsClass.GetRandomDir() * scale);

        /// <summary>Moves entity by <c>pos</c>, relative to it's current position</summary>
        ///Overrides any exsisting destination
        private void MoveRelative(Vector3 pos) => navigation.SetDestination(gameObject.transform.position + pos);

        #endregion movement
      
        /// <summary>Defines how close a rabbit must be to food to be able to eat it.</summary>
            public readonly int EAT_DISTANCE = 3;
        #region health
        /// <summary>Base speed for entities food consumption</summary>
        public int baseMetabolism;

        /// <summary>Base health</summary>
        public int baseHealth;
        #endregion   

        #region public static methods

        private static readonly string IENTITY_ASSERSION_FAILIURE = "[IENTITY] Implemented statemachine behaviour requires IEntity, but none was located!";
        public static IEntity getAnimatorIEntity(Animator animator) {
            IEntity super = animator.gameObject.GetComponent<IEntity>();
            if (super == null) {Debug.LogError(IENTITY_ASSERSION_FAILIURE); Assert.IsNotNull(super, IENTITY_ASSERSION_FAILIURE);}
            return super;
        }

        #endregion
    }
}