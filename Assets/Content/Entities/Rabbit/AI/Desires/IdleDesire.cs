using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using System;
using UnityEngine.AI;
using utility;

 [Serializable]
public class IdleDesire : Desire {
       [HideInInspector] private GameObject foodTarget;

       public float distanceFromFood {get; private set;} = -1f;

       

        public IdleDesire() : base(5f,0f,"Idle"){
        }

        /// <inheritdocs>
        /// Idle is top priority, ran framely.
        public override void executeUpdate(){

          
        }

        /// <inheritdocs>
        /// Idle has just become top priority
        public override void executeEnter(){
            randomLocation();
        }

        /// <inheritdocs>
        /// Idle is not longer top priority
        public override void executeExit(){
            
        }

        private void randomLocation(){
            if (UnityEngine.Random.Range(0,10) > 5){
                Parent.moveController.controlTarget.SetDestination(GameObject.Find("Rabbit Burrow").transform.position);
            } else {
                Parent.moveController.controlTarget.SetDestination(tools.randomInTransform(GameObject.Find("World").transform, true));
            }
            
        }


}