using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class IdleState : StateMachineBehaviour
    {
		IEntity parent;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
			parent = IEntity.getIEntity(animator);
			
 			if (parent.metabolism.isHungry) {animator.SetTrigger(Literals.ST_TRIG_DO_HUNT); return;}
			
			
			animator.SetTrigger(Literals.ST_TRIG_DO_EXPLORE);
        }
    }
}