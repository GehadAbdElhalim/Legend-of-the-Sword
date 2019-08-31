using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    Animator animator;
    public GameObject Swoerd;
    public GameObject Legendary;
    GameObject[] Slashes;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Swoerd.GetComponent<Collider>().enabled = false;
        Legendary.GetComponent<Collider>().enabled = false;
        Slashes = transform.parent.GetComponent<Movement>().Slashes;
    }

    public void ColliderOpen()
    {
        Swoerd.GetComponent<Collider>().enabled = true;
        Legendary.GetComponent<Collider>().enabled = true;
    }

    public void ColliderClose()
    {
        Swoerd.GetComponent<Collider>().enabled = false;
        Legendary.GetComponent<Collider>().enabled = false;
    }

    public void Cancel()
    {
        Movement.canGetInput = false;
        animator.SetInteger("Combo", Movement.comboCounter);
        if (Movement.UseLegendary)
        {
            GameObject slash = Slashes[Random.Range(0, Slashes.Length)];
            GameObject instance = Instantiate(slash, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), slash.transform.rotation);
            instance.transform.up = transform.parent.transform.forward;
            instance.GetComponent<Rigidbody>().AddForce(instance.transform.up * 10, ForceMode.Impulse);
        }
    }

    public void FootStep()
    {
        GetComponent<AudioSource>().Play();
    }
}
