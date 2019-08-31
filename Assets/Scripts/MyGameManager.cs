using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGameManager : MonoBehaviour
{
    public static int GameState = 0;
    public GameObject Player;
    public GameObject MeleeEnemy;
    
    public Transform[] SpawnPositionsMelee;
    public Transform[] SpawnPositionsRanged;
    public Transform[] SpawnPositionsMixed;

    public Image BlackScreen;

    public static bool inConversation;

    public static int ConversationNumber = 0;

    private void Start()
    {
        BlackScreen.GetComponent<Animator>().SetBool("GoBlack", true);
        FindObjectOfType<DialogueManager>().StartDialogue(0);
        inConversation = true;
        ConversationNumber = 0;
    }



    public void StratGameState(int State)
    {
        if (State == 0)
        {

        }
    }
}
