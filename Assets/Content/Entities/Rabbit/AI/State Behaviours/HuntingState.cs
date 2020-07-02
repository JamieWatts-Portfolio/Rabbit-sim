
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

namespace AI
{
	/// <summary> State machine behavour to locate and consume edible game objects</summary>
	public class HuntingState : StateMachineBehaviour
	{
		#region Inspector properties    
		/// <summary> Inspector set list of tags which are concidered food.</summary> 
		[Tooltip("String tags that are concidered food.")] 
		public List<String> edibleTags = new List<String>();

		/// <summary>Defines how close a entity must be to food to be able to eat it.</summary>
		[Tooltip("How close entity must be to target food origin to be able to eat it.")]
		public int eatDistance = 5;
		#endregion

		#region Private properties
		/// <summary> Current food target. </summary> 
		private GameObject foodTarget;

		public Edible edible {get; private set;}

		/// <summary> Meters from food target, as calculated in the last frame </summary> 
		public float distanceFromFoodTarget { get; private set; } = -1f;

		/// <summary> Meters from food target, as calculated in the last frame </summary> 
		public float lastDistanceFromFoodTarget { get; private set; } = -1f;

		/// <summary> IEntity this state controls </summary> 
		private IEntity parent = null;

		/// <summary> statemachine this state is controlled by </summary> 
		private Animator animator = null;
		#endregion

		#region State

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
			Configure(animator);
			// store the ai controller
			if (foodTarget == null) { animator.SetTrigger(Literals.ST_TRIG_IDLE_RETURN); return; }                  // there is no food, exit state
	
			parent.navigation.SetDestination(foodTarget.transform.position);
			parent.navigation.speed = parent.baseSpeed;
		}

		private void Configure(Animator stateMachine)
		{
			animator = stateMachine;
			parent = IEntity.getIEntity(animator);
			tryFindTarget();
		}
		
		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
			checkEat();
		#endregion

		#region Hunting behaviour
		/// <summary> </summary>
		private void tryFindTarget()
		{
			foodTarget = null;
			foreach (String tag in edibleTags){
				foodTarget = GameObject.FindWithTag(tag); 				// try to find en edible tagged object.
				if (foodTarget != null) {
					edible = foodTarget.GetComponent<Edible>();			// fetch edible monocomp from food.
					if (edible == null) continue;						// Target has no edible component; it's not actually an edible object.
					return;												// We have a valid food target, return.
				}
			}
			exitState();												// No valid edibles were found, exit state.
		}

		/// <summary> Checks if we have a valid food target. </summary>
		/// If eat conditions are met, eat event is automatically invoked.
		private void checkEat()
		{
			if (foodTarget == null) {exitState(); return; }															// There is no target, reject eat attempt.
			updateDistance();																						// Figure out how far we are from the target
			if (!(distanceFromFoodTarget < eatDistance)) return;												    // If we're not close enough, reject call.
			checkStuck();
			eat();														// If we're close enough, eat it.
		}

		public void updateDistance() {
			lastDistanceFromFoodTarget = distanceFromFoodTarget;
			distanceFromFoodTarget = Vector3.Distance(foodTarget.transform.position, parent.transform.position);    
		}
		
		public void checkStuck()
		{

		}

		/// <summary> Eats target food, handles saturation, animation and particles. </summary>
		private void eat()
		{
			parent.metabolism.modSaturation(foodTarget.GetComponent<Edible>().Eat());	// Invoke eat() to destroy object and get saturation, parse saturation to entity metabolism.
			onEat();
		}


		/// <summary> Post eat event. triggers return to idle and entity side animations / particles. </summary>
		private void onEat()
		{
			// reset saturation
			// TODO Eat animation, particles etc.
			exitState();
		}
		
		/// <summary> Sets statemachine trigger for return to idle state.</summary> 
		private void exitState() => animator.SetTrigger(Literals.ST_TRIG_IDLE_RETURN);
		#endregion
	}
}