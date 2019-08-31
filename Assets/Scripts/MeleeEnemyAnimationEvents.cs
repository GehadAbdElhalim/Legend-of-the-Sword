using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAnimationEvents : MonoBehaviour
{
    public GameObject spear;

    private void Start()
    {
        spear.GetComponent<Collider>().enabled = false;
    }

    public void ColliderOpen()
    {
        spear.GetComponent<Collider>().enabled = true;
    }

    public void ColliderClose()
    {
        spear.GetComponent<Collider>().enabled = false;
    }

    public void FootStep()
    {
        GetComponent<AudioSource>().Play();
    }
}
