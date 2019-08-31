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
    public Transform[] checkpoints;
    public GameObject[] Triggers;

    public Image PlayerHealthBar;
    public Image PlayerDashBar;
    public GameObject mainCam;

    public static bool PlayerDead = false;

    public Image BlackScreen;

    public static bool inConversation;

    public static int ConversationNumber = 0;

    public static bool justEndedConversation = false;

    private void Start()
    {
        BlackScreen.GetComponent<Animator>().SetBool("GoBlack", true);
        FindObjectOfType<DialogueManager>().StartDialogue(0);
        inConversation = true;
        ConversationNumber = 0;
    }

    private void Update()
    {
        if (!inConversation && justEndedConversation)
        {
            justEndedConversation = false;
            BlackScreen.GetComponent<Animator>().SetBool("GoBlack", false);
        }

        if (PlayerDead)
        {
            GameObject instance = Instantiate(Player, checkpoints[GameState].transform.position, Quaternion.identity);
            Player.transform.forward = checkpoints[GameState].forward;
            Player.GetComponent<Movement>().camera = mainCam;
            Player.GetComponent<PlayerHealthBar>().CooldownBar = PlayerDashBar;
            Player.GetComponent<PlayerHealthBar>().healthBar = PlayerHealthBar;
            mainCam.GetComponent<ThirdPersonCamera>().target = instance.transform;
            Triggers[GameState].GetComponent<Collider>().enabled = true;
            PlayerDead = false;
        }
    }

    public void StratGameState(int State)
    {
        if (State == 0)
        {

        }
    }

    public void IncrementState()
    {
        if (GameState < 3)
        {
            GameState += 1;
            Triggers[GameState].GetComponent<Collider>().isTrigger = true;
        }
    }
}
