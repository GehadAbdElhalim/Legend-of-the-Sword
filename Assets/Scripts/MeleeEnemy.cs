using System.Collections;
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
    bool checkrest = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(1).GetComponent<Animator>();
        agent.SetDestination(target.transform.position);
    }

    private void Update()
    {
        anim.SetBool("Run", !agent.isStopped);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            agent.isStopped = true;
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
            if(checkrest && gos.Length <= 1)
            {
                FindObjectOfType<MyGameManager>().IncrementState();
                checkrest = false;
            }
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            agent.isStopped = true;
        }

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
        transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y , target.transform.position.z));
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
