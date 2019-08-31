using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyHealthSystem : MonoBehaviour
{
    public float Health;
    public float currentHealth;
    Animator anim;

    void Start()
    {
        anim = transform.GetChild(1).GetComponent<Animator>();
        currentHealth = Health;
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerArrow")
        {
            Destroy(other.gameObject);
            if (currentHealth > 0)
            {
                currentHealth -= 40;
                if (currentHealth > 0)
                {
                    anim.SetTrigger("Hurt");
                }
                else
                {
                    anim.SetInteger("Die", anim.GetInteger("Die") + 1);
                    Invoke("Die", 2f);
                }
            }
        }

        if (other.tag == "sword")
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
                    Invoke("Die", 2);
                }
            }
        }

        if (other.tag == "slash")
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
                    Invoke("Die", 2);
                }
            }
        }
    }
}
