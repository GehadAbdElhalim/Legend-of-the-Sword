using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float Health;
    public float currentHealth;
    Animator anim;
    public AudioClip StabSword;
    public AudioClip Slash;
    public AudioClip StabMetal;


    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
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
            if (currentHealth > 0)
            {
                currentHealth -= 5;
                if (currentHealth > 0)
                {
                    anim.SetTrigger("Hurt");
                }
                else
                {
                    anim.SetInteger("Die", anim.GetInteger("Die") + 1);
                    Invoke("Die", 2f);
                }
                GetComponent<AudioSource>().PlayOneShot(StabMetal);
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
                    Invoke("Die", 2f);
                }
                GetComponent<AudioSource>().PlayOneShot(StabSword);
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
                    Invoke("Die", 2f);
                }
                GetComponent<AudioSource>().PlayOneShot(Slash);
            }
        }
    }
}
