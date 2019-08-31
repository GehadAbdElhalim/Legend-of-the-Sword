using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MyGameManager : MonoBehaviour
{
    public static int GameState = 0;
    public GameObject Player;
    public GameObject MeleeEnemy;
    public GameObject RangedEnemy;
    public GameObject Boss;

    public Transform[] EnemyPositionsInArena;
    public Transform PlayerPositionInArena;
    public Transform BossPosition;
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

    public GameObject DummySword;



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
            if(ConversationNumber == 3)
            {
                SceneManager.LoadScene(0);
            }
        }

        if (PlayerDead)
        {
            print(GameState);
            if (GameState < 3)
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

            if (GameState == 3)
            {
                GameObject instance = Instantiate(Player, PlayerPositionInArena.position, Quaternion.identity);
                Player.transform.forward = PlayerPositionInArena.forward;
                Player.GetComponent<Movement>().camera = mainCam;
                Player.GetComponent<PlayerHealthBar>().CooldownBar = PlayerDashBar;
                Player.GetComponent<PlayerHealthBar>().healthBar = PlayerHealthBar;
                mainCam.GetComponent<ThirdPersonCamera>().target = instance.transform;
                PlayerDead = false;
                PositionPlayerInArena1();
            }

            if(GameState == 4)
            {
                GameObject instance = Instantiate(Player, PlayerPositionInArena.position, Quaternion.identity);
                Player.transform.forward = PlayerPositionInArena.forward;
                Player.GetComponent<Movement>().camera = mainCam;
                Player.GetComponent<PlayerHealthBar>().CooldownBar = PlayerDashBar;
                Player.GetComponent<PlayerHealthBar>().healthBar = PlayerHealthBar;
                mainCam.GetComponent<ThirdPersonCamera>().target = instance.transform;
                PlayerDead = false;
                Destroy(GameObject.FindGameObjectWithTag("Boss").gameObject);
                PositionPlayerInArena2();
            }
        }
    }

    public void IncrementState()
    {
        print(GameState);
        if (GameState < 4)
        {
            GameState += 1;
            Triggers[GameState].GetComponent<Collider>().isTrigger = true;
        }

        if (GameState == 4)
        {
            BlackScreen.GetComponent<Animator>().SetBool("GoBlack", true);
            FindObjectOfType<DialogueManager>().StartDialogue(2);
            inConversation = true;
            ConversationNumber = 2;
            PositionPlayerInArena2();
        }

        if(GameState == 5)
        {
            BlackScreen.GetComponent<Animator>().SetBool("GoBlack", true);
            FindObjectOfType<DialogueManager>().StartDialogue(3);
            inConversation = true;
            ConversationNumber = 3;
        }
    }

    public void PositionPlayerInArena1()
    {
        GameObject PlayerInstance = GameObject.FindGameObjectWithTag("Player");
        PlayerInstance.transform.position = PlayerPositionInArena.position;
        PlayerInstance.transform.forward = PlayerPositionInArena.forward;
        Movement.UseLegendary = true;

        for(int i = 0; i < EnemyPositionsInArena.Length; i++)
        {
            if(i%2 == 0)
            {
                GameObject go = Instantiate(RangedEnemy, EnemyPositionsInArena[i].position, Quaternion.identity);
                go.transform.LookAt(new Vector3(PlayerInstance.transform.position.x, 0, PlayerInstance.transform.position.z));
                go.GetComponent<RangedEnemy>().target = PlayerInstance.gameObject;
            }
            else
            {
                GameObject go = Instantiate(MeleeEnemy, EnemyPositionsInArena[i].position, Quaternion.identity);
                go.transform.LookAt(new Vector3(PlayerInstance.transform.position.x, 0, PlayerInstance.transform.position.z));
                go.GetComponent<MeleeEnemy>().target = PlayerInstance.gameObject;
            }
        }

    }

    public void PositionPlayerInArena2()
    {
        GameObject PlayerInstance = GameObject.FindGameObjectWithTag("Player");
        PlayerInstance.transform.position = PlayerPositionInArena.position;
        PlayerInstance.transform.forward = PlayerPositionInArena.forward;
        Movement.UseLegendary = false;

        Instantiate(Boss, BossPosition.position, Quaternion.identity);
        Boss.GetComponent<BossScript>().target = PlayerInstance;
    }
}
