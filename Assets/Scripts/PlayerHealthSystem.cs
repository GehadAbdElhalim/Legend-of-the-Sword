using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public float Health;
    public float currentHealth;
    Animator anim;

    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        currentHealth = Health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "arrow")
        {
            if (currentHealth > 0)
            {
                currentHealth -= 5;
                if (currentHealth > 0)
                {
                    anim.SetTrigger("Hurt");
                }
                else
                {
                    anim.SetInteger("Die", anim.GetInteger("Die")+1);
                }
            }
        }

        if (other.tag == "spear")
        {
            if (currentHealth > 0)
            {
                currentHealth -= 10;
                if (currentHealth > 0)
                {
                    anim.SetTrigger("Hurt");
                }
                else
                {
                    anim.SetInteger("Die", anim.GetInteger("Die") + 1);
                }
            }
        }

        if (other.tag == "EnemySlash")
        {
            if (currentHealth > 0)
            {
                currentHealth -= 20;
                if (currentHealth > 0)
                {
                    anim.SetTrigger("Hurt");
                }
                else
                {
                    anim.SetInteger("Die", anim.GetInteger("Die") + 1);
                }
            }
        }
    }
}
