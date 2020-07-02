using UnityEngine;
using UnityEngine.AI;
using CodeMonkey.Utils;
using System.Diagnostics;

namespace AI
{
    /// <summary>Base entity values and interactions</summary>
    [RequireComponent(typeof(MeshCollider))]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Metabolism))]
    public class IEntity : MonoBehaviour
    {
			
        #region movement
        /// <summary>Inspector set nav mesh agent<summary>
        /// Basis for all ientity movement.
        [SerializeField] public NavMeshAgent navigation;

        /// <summary>Base movement speet</summary>
        public int baseSpeed { get; private set; } = 10;

        /// <summary>speed modifyer</summary>
        /// Multiplier for speed
        /// <c>speed = base speed * speed modifyer<c>
        public int speedMod { get; set; } = 10;

        /// <summary>Base stamina</summary>
        public int baseStamina;

        /// <summary>Distance for senory reception in entities</summary>
        /// Includes sight and smell
        public int senseDistance;

        /// <summary>sense modifyer</summary>
        /// Multiplier for sense
        /// <c>sence = base sense * sense modifyer<c>
        public int senseMod { get; set; }

        /// <summary>Moves the parent entity is a random direction</summary>
        /// Overrides any exsisting destination
        /// <param name="scale">Scale exponent. Random movement is scaled by this.</param>
        public void MoveRandom(byte scale) => MoveRelative(UtilsClass.GetRandomDir() * scale);

        /// <summary>Moves entity by <c>pos</c>, relative to it's current position</summary>
        ///Overrides any exsisting destination
        private void MoveRelative(Vector3 pos) => navigation.SetDestination(gameObject.transform.position + pos);

        #endregion movement

        #region Health and Hunger
        /// <summary>Metabolism behaviour for this entity.</summary>
        public Metabolism metabolism = null;

        /// <summary>Base speed for entities food consumption</summary>
        public int baseMetabolism;

        /// <summary>Base health</summary>
        public int baseHealth;
        #endregion   

        #region public utility methods

        /// <summary>returns component of specified type from parent object of a statemachine</summary>
        public static T getAIComponent<T>(Animator animator) => getAIComponent<T>(animator.gameObject);

        /// <summary>Returns component of specified type from this entity instance.</summary>
        public T getAIComponent<T>() => getAIComponent<T>(gameObject);

		/// <summary>Returns IEntity component from an monocomp's parnent object.</summary>
        public static IEntity getIEntity(Animator component) => getAIComponent<IEntity>(component);

        private static readonly string IENTITY_ASSERSION_FAILIURE = "[IENTITY] An AI Component was requested, but no matching component was found!";
		public static T getAIComponent<T>(GameObject IEntityInstance) {
			T component = IEntityInstance.gameObject.GetComponent<T>();
            if (component == null) {UnityEngine.Debug.LogError(IENTITY_ASSERSION_FAILIURE); Debugger.Break();}
            return component;
		} 
        #endregion

        #region Monocomponent methods
        public void Start()
        {
            metabolism = getAIComponent<Metabolism>();
        }

        #endregion
    }
}