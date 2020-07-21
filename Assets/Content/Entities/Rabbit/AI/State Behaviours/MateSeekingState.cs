using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
	/// <summary> Generic entity mate seeking state machine behaviour.</summary>
	public class MateSeekingState : StateMachineBehaviour
	{
		#region inspector properties

		/// <summary> Object tag that marks a compatable mate. </summary>
		[Tooltip("Object tag that marks a compatable mate.")]
		public string compatableTag = "";

		/// <summary> Max time taken to mate (s) </summary>
		[Tooltip("")]
		public byte maxMateTime = 4;

		/// <summary> Maximum time this state will spend seeking a mate.!-- </summary> 
		[Tooltip("")]
		public byte maxMateSeekTime = 60;

		/// <summary></summary>
		[Tooltip("")]
		public byte maxMateSeekRadius = 5;
		#endregion


		/// <summary> </summary>
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{

		}

		/// <summary> </summary>
		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{

		}



	}
}