using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDoorScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<DialogueManager>().StartDialogue(1);
        }
    }
}
