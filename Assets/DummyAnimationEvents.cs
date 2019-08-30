using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAnimationEvents : MonoBehaviour
{
    public GameObject Legendary;

    private void Start()
    {
        Legendary.GetComponent<Collider>().enabled = false;
    }

    public void ColliderOpen()
    {
        Legendary.GetComponent<Collider>().enabled = true;
    }

    public void ColliderClose()
    {
        Legendary.GetComponent<Collider>().enabled = false;
    }

    public void Cancel()
    {
        
    }
}
