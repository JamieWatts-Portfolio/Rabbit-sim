using UnityEngine;
using UnityEngine.AI;


namespace AI {

  
    /// <summary>Base entity value and interactions</summary>
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

        #endregion movement
      
        /// <summary>Defines how close a rabbit must be to food to be able to eat it.</summary>
            public readonly int EAT_DISTANCE = 3;
        #region health
        /// <summary>Base speed for entities food consumption</summary>
        public int baseMetabolism;

        /// <summary>Base health</summary>
        public int baseHealth;
        #endregion   
    }


}