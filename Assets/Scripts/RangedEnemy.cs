﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;
    private bool attacking = false;
    Animator anim;
    public float timeBetweenAttack;
    bool endingAttack = false;
    public GameObject arrow;
    public int minDistance = 2;
    public float speedFactor = 0.05f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        agent.SetDestination(target.transform.position);
    }

    private void Update()
    {

        //if(anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        //{

        //}
        double dist = (transform.position - target.transform.position).magnitude;
        anim.SetBool("Run", !agent.isStopped);
        if (dist<minDistance){
            transform.LookAt(target.transform.forward*-1);
            this.transform.Translate(this.transform.forward*speedFactor);

             anim.SetBool("Run", true);
             return;
        }
        if (!attacking && dist>minDistance)
        {
            if (dist <= agent.stoppingDistance)
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
        GameObject projectile = Instantiate(arrow,new Vector3(transform.position.x,0.5f,transform.position.z),Quaternion.identity);
        projectile.transform.forward = transform.forward;
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 10, ForceMode.Impulse);
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
