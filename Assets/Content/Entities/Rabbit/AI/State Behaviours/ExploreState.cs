using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using System;
using UnityEngine.AI;
using utility;

[Serializable]
public class ExploreDesire : StateMachineBehaviour {
    [HideInInspector] private GameObject foodTarget;

    [SerializeField] IEntity parentEntity = null;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (parentEntity.navigation.pathStatus == NavMeshPathStatus.PathComplete) {                                         // If previous navigation status is complete 
            if (UnityEngine.Random.Range(0,10) > 5){                                                                        // Random weight,
                parentEntity.navigation.SetDestination(GameObject.Find("Rabbit Burrow").transform.position);                // Go to a burrow
            } else {
                parentEntity.MoveRandom(10);                                                                                // Got to a random position
            }
        }                                                                                                                   // Else we're still pathing, return.
    }


}
