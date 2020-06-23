using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetabolismBehaviour : StateMachineBehaviour
{
    /// <summary>Current level representing the entitie's hunger.</summary>
    public float saturation = 100;

    /// <summary>Maximum hunger level</summary>
    [Tooltip("Maximum total saturation")]
    public byte maxSaturation = 100;

    /// <summary>Level at which the entitiy becomes hungry</summary>
    [Tooltip("Level at which the entitiy becomes hungry")]
    public float hungerThreshold = 60;

    /// <summary>Is the entity currently hungry</summary>
    private bool isHungry;

    /// <summary>How much the saturation is decreased every frame</summary>
   public float metabolismRate = 0.01f;
   
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        checkConfiguration();
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        metabolise();
        checkThreshold();

        animator.SetBool(Literals.ST_PARAM_HUNGRY, isHungry);       //Broadcast hungry state to statemachine.
        Debug.Log("sat: " + saturation);
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
       
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    /// <summary> Ensures the configuration is valid. </summary>
    private void checkConfiguration(){
        saturation = assertInRange(saturation, maxSaturation, 0);
        hungerThreshold = assertInRange(hungerThreshold, maxSaturation, 0);
    }

    /// <summary>Set <c>isHungry<c> if saturation is below hunger threshold</summary>
    private void checkThreshold() => isHungry = (saturation < hungerThreshold);

    /// <summary>Decreses saturation by metabolism rate</summary>
    private void metabolise() => saturation += -metabolismRate;

    /// <summary>Ensures that <param>value<param> is between <param>max<param> and <param>min<param></summary>
    /// <returns>returns value normalised to range</returns>
    private float assertInRange(float value, float max, float min) => (value > max) ? max : (value < min) ? 0 : value; 
}
