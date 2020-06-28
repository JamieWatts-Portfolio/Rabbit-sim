using UnityEngine;
using AI;
using System;

namespace Rabbit{

    [Serializable]
    public class FoodDesire : Desire {
       [HideInInspector] private GameObject foodTarget;

       public float distanceFromFood {get; private set;} = -1f;

        public FoodDesire() : base(1f,0.01f,"Food"){
        }

        /// <inheritdocs>
        /// Food is top priority, ran framely.
        public override void executeUpdate(){
            checkEat();
        }

        /// <inheritdocs>
        /// Food has just become top priority
        public override void executeEnter(){                                                                             // store the ai controller
            foodTarget = (foodTarget == null) ? GameObject.FindWithTag(Literals.TAG_CARROT) : foodTarget;   // If we have no food target, try to find a carrot.
            if (foodTarget == null) {Parent.controls.entityDesires.reset(this); return;}                    // there is no food, reset desire.  

            Parent.moveController.controlTarget.SetDestination(foodTarget.transform.position);
            Parent.moveController.controlTarget.speed = Parent.controls.entity.baseSpeed; 
        }

        /// <inheritdocs>
        /// Food is not longer top priority
        public override void executeExit(){
            
        }
        private void checkEat(){
            if (foodTarget == null) return;

            distanceFromFood = Vector3.Distance(foodTarget.transform.position, Parent.controls.entity.transform.position);    // Find how far are are from the food target.                                                                                                            // if we can't see it, don't target it.
            Debug.Log("Distance from food: " + distanceFromFood);
            if (distanceFromFood < Parent.controls.entity.EAT_DISTANCE) eat();
        }

        private void eat(){
            GameObject.Destroy(foodTarget);
            onEat();
        }

        private void onEat(){
            Parent.controls.entityDesires.reset(this);
        }

        


    }
}