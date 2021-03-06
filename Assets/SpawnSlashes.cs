﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlashes : StateMachineBehaviour
{
    public GameObject[] Slashes;
    GameObject Slasher;
    GameObject Target;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Slasher = GameObject.FindGameObjectWithTag("Boss");
        Target = GameObject.FindGameObjectWithTag("Player");
        GameObject Slash = Slashes[Random.Range(0, Slashes.Length)];
        GameObject instance = Instantiate(Slash, Slasher.transform.position + new Vector3(0, 0.5f, 0), Slash.transform.rotation);
        //instance.transform.LookAt(new Vector3(0, 0.5f, 0) + Target.transform.position);
        instance.transform.up = Slasher.transform.forward;
        instance.GetComponent<Rigidbody>().AddForce(instance.transform.up * 10, ForceMode.Impulse);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
