using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaDoorScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<MyGameManager>().BlackScreen.GetComponent<Animator>().SetBool("GoBlack", true);
            FindObjectOfType<DialogueManager>().StartDialogue(1);
            MyGameManager.inConversation = true;
            MyGameManager.ConversationNumber = 0;
            FindObjectOfType<MyGameManager>().PositionPlayerInArena1();
            MyGameManager.GameState = 4;
        }
    }
}
