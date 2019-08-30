using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject target;
    public GameObject Legendary;
    public float waitTime;
    float currentTime;
    Animator animator;
    bool LongAttack = true;
    public float LongAttackduration;

    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        transform.LookAt(new Vector3(target.transform.position.x, 0, target.transform.position.z));

        if ( currentTime <= 0 )
        {
            currentTime = 0;
            Attack();
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
    }

    void Attack()
    {
        if (LongAttack)
        {
            animator.SetBool("LongAttack", true);
            Invoke("EndLongAttack", 3);
        }
        else
        {
            animator.SetTrigger("ShortAttack");
            currentTime = waitTime;
            LongAttack = true;
        }
    }

    void EndLongAttack()
    {
        LongAttack = false;
        animator.SetBool("LongAttack", false);
        currentTime = waitTime;
    }
}
