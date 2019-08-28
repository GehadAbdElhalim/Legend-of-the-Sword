using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyArrow", timer);
    }

    void DestroyArrow()
    {
        Destroy(this.gameObject);
    }
}
