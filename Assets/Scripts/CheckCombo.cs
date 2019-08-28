using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCombo : StateMachineBehaviour
{
    public bool lastmove;
    bool pressed = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pressed = false;
        Movement.canGetInput = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(pressed);
        if (Movement.canGetInput && !pressed && Input.GetMouseButtonDown(0) && !lastmove)
        {
            Movement.comboCounter += 1;
            pressed = true;
            Movement.canGetInput = false;
        } 
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (lastmove)
        //{
        //    animator.SetBool("Combat", false);
        //}
        //else
        //{
        if (pressed == false)
        {
            Movement.inCombat = false;
            Movement.comboCounter = 0;
            animator.SetInteger("Combo", 0);
        }
        else
        {
            pressed = false;
        }
        //}
        //if (lastmove)
        //{
        //    PlayerController.HeavyCombo = 0;
        //}
    }

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
