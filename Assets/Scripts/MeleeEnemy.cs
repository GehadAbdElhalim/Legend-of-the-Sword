﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;
    private bool attacking = false;
    Animator anim;
    public float timeBetweenAttack;
    bool endingAttack = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        agent.SetDestination(target.transform.position);
    }

    private void Update()
    {
        anim.SetBool("Run", !agent.isStopped);

        //if(anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        //{

        //}

        if (!attacking)
        {
            if ((transform.position - target.transform.position).magnitude <= agent.stoppingDistance)
            {
                agent.isStopped = true;
                GetComponent<Rigidbody>().isKinematic = true;
                transform.LookAt(target.transform.position);
                Attack();
            }
            else
            {
                agent.isStopped = false;
                GetComponent<Rigidbody>().isKinematic = false;
                agent.SetDestination(target.transform.position);
            }
        }

        if (anim.GetInteger("Attack") == 2)
        {
            Invoke("notAttacking", timeBetweenAttack);
            anim.SetInteger("Attack", 0);
        }
    }

    void Attack()
    {
        attacking = true;
        transform.LookAt(new Vector3(target.transform.position.x, 0, target.transform.position.z));
        //attack animation
        anim.SetInteger("Attack",1);
    }

    public void EndAttacking()
    {
        Invoke("notAttacking", timeBetweenAttack);
    }

    public void notAttacking()
    {
        attacking = false;
    }
}