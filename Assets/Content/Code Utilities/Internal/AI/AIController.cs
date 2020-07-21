using UnityEngine;
using UnityEditor;
using System;


namespace AI {

[System.Serializable]
///<summary>instantiable generic ai for animal entities.</summary>
public class AIController : MonoBehaviour {

    /// <summary> Unity editor components </summary>
    /// AIController is a component intended for adding via the unity editor.
    /// Region is for editor fields and collections.
    #region editor fields

    [Serializable]
    /// <summary>Collection of controlls intended for unity editor in one companion class.</summary>
    [HideInInspector] public class AIControllerEditor {

        /// <summary>Navigation mesh agent of the world</summary>
        /// used for AI based movement navigation.
        public UnityEngine.AI.NavMeshAgent NavigationAgent;

        /// <summary>Parent entity under control of this controller.</summary>
        public IEntity entity;

        /// <summary>Desire definitions of the entity under control.</summary>
        [SerializeField] public Priority entityDesires;
    }

    /// <summary>Editor and internally accessable collection of controls for this AI.</summary>
    [SerializeField] public AIControllerEditor controls = new AIControllerEditor();

    #endregion




    public void Update(){
        if (!enabled) return;
        controls.entityDesires.update();
    }

    /// <summary>Create a new AI using monobehaviour<summary>
    public void Start(){
        // Check configuration

        enabled = false;                                           // Disable monobehaviour
        if (controls.entity == null) {Debug.LogError("AIController with no entity class assigned! AI Disabled."); return;}
        if (controls.entityDesires == null) {Debug.LogError("AIController with no desires assigned! AI Disabled."); return;}
        if (controls.NavigationAgent == null) {Debug.LogError("AIController with no nav mesh agent! AI Disabled."); return;} 
        enabled = true;                                            // Enable monobehaviour updates if configured correctly. Only reached if configuration is valid.

        // Setup properties
        controls.entityDesires.parent = this;
	    }
    }
}