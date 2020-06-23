using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using AI;
using UnityEngine.AI;

public class IdleBehaviour : StateMachineBehaviour
{

    [HideInInspector] private IEntity parentEntity;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        parentEntity = animator.GetComponent<IEntity>();
        setIdleLocation();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (parentEntity.navigation.remainingDistance != Mathf.Infinity
              && parentEntity.navigation.pathStatus == NavMeshPathStatus.PathComplete
              && parentEntity.navigation.remainingDistance == 0)
            setIdleLocation();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    /// <summary>Sets new idle destination </summary>
    /// Chance to go in a random direction, or visit a burrow.
    private void setIdleLocation()
    {
        if (UnityEngine.Random.Range(0, 10) > 5)                                        // Randomly choose to target a carrot, or random movement. 50/50.
        {                                                                               // If random,
            MoveRelative(RandomDirection());                                            // Move randomly.
        }
        else                                                                            // otherwise
        {
            GameObject targetCarrot = GameObject.FindWithTag(Literals.TAG_BURROW);      // try to find a carrot to target by tag.
            if (targetCarrot != null)                                                   // if there's a carrot to target,
                parentEntity.navigation.SetDestination(targetCarrot.transform.position);// set it as destination,
            else                                                                        // otherwise
                MoveRelative(RandomDirection());                                        // Move randomly.
                
        }
    }

    private Vector3 RandomDirection() => UtilsClass.GetRandomDir() * 10;

    private void MoveRelative(Vector3 pos) => parentEntity.navigation.SetDestination(parentEntity.transform.position + pos);

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
