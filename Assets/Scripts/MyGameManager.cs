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

    public AudioSource audio1;

    public AudioSource audio2;

    public AudioSource audio3;

    private void Start()
    {
        audio1 = GetComponents<AudioSource>()[0];
        audio2 = GetComponents<AudioSource>()[1];
        audio3 = GetComponents<AudioSource>()[2];
        audio1.Play();
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

        //if (PlayerDead)
        //{
        //    print(GameState);
        //    if (GameState < 3)
        //    {
        //        GameObject instance = Instantiate(Player, checkpoints[GameState].transform.position, Quaternion.identity);
        //        Player.transform.forward = checkpoints[GameState].forward;
        //        Player.GetComponent<Movement>().camera = mainCam;
        //        Player.GetComponent<PlayerHealthBar>().CooldownBar = PlayerDashBar;
        //        Player.GetComponent<PlayerHealthBar>().healthBar = PlayerHealthBar;
        //        mainCam.GetComponent<ThirdPersonCamera>().target = instance.transform;
        //        Triggers[GameState].GetComponent<Collider>().enabled = true;
        //        PlayerDead = false;
        //    }

        //    if (GameState == 3)
        //    {
        //        GameObject instance = Instantiate(Player, PlayerPositionInArena.position, Quaternion.identity);
        //        Player.transform.forward = PlayerPositionInArena.forward;
        //        Player.GetComponent<Movement>().camera = mainCam;
        //        Player.GetComponent<PlayerHealthBar>().CooldownBar = PlayerDashBar;
        //        Player.GetComponent<PlayerHealthBar>().healthBar = PlayerHealthBar;
        //        mainCam.GetComponent<ThirdPersonCamera>().target = instance.transform;
        //        PlayerDead = false;
        //        PositionPlayerInArena1();
        //    }

        //    if(GameState == 4)
        //    {
        //        GameObject instance = Instantiate(Player, PlayerPositionInArena.position, Quaternion.identity);
        //        Player.transform.forward = PlayerPositionInArena.forward;
        //        Player.GetComponent<Movement>().camera = mainCam;
        //        Player.GetComponent<PlayerHealthBar>().CooldownBar = PlayerDashBar;
        //        Player.GetComponent<PlayerHealthBar>().healthBar = PlayerHealthBar;
        //        mainCam.GetComponent<ThirdPersonCamera>().target = instance.transform;
        //        PlayerDead = false;
        //        Destroy(GameObject.FindGameObjectWithTag("Boss").gameObject);
        //        PositionPlayerInArena2();
        //    }
        //}
    }

    public void RevivePlayer()
    {
        if (PlayerDead)
        {
            print(GameState);
            if (GameState < 3)
            {
                print("hi");
                GameObject instance = Instantiate(Player, checkpoints[GameState].transform.position, Quaternion.identity);
                print(GameObject.FindGameObjectWithTag("Player"));
                instance.transform.forward = checkpoints[GameState].forward;
                instance.GetComponent<Movement>().camera = mainCam;
                instance.GetComponent<PlayerHealthBar>().CooldownBar = PlayerDashBar;
                instance.GetComponent<PlayerHealthBar>().healthBar = PlayerHealthBar;
                mainCam.GetComponent<ThirdPersonCamera>().target = instance.transform;
                Triggers[GameState].GetComponent<Collider>().enabled = true;
                PlayerDead = false;
            }

            if (GameState == 3)
            {
                GameObject instance = Instantiate(Player, PlayerPositionInArena.position, Quaternion.identity);
                instance.transform.forward = PlayerPositionInArena.forward;
                instance.GetComponent<Movement>().camera = mainCam;
                instance.GetComponent<PlayerHealthBar>().CooldownBar = PlayerDashBar;
                instance.GetComponent<PlayerHealthBar>().healthBar = PlayerHealthBar;
                mainCam.GetComponent<ThirdPersonCamera>().target = instance.transform;
                PlayerDead = false;
                PositionPlayerInArena1();
            }

            if (GameState == 4)
            {
                GameObject instance = Instantiate(Player, PlayerPositionInArena.position, Quaternion.identity);
                instance.transform.forward = PlayerPositionInArena.forward;
                instance.GetComponent<Movement>().camera = mainCam;
                instance.GetComponent<PlayerHealthBar>().CooldownBar = PlayerDashBar;
                instance.GetComponent<PlayerHealthBar>().healthBar = PlayerHealthBar;
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
            FindObjectOfType<MyGameManager>().audio3.Pause();
            FindObjectOfType<MyGameManager>().audio1.UnPause();
        }

        if (GameState == 4)
        {
            BlackScreen.GetComponent<Animator>().SetBool("GoBlack", true);
            FindObjectOfType<DialogueManager>().StartDialogue(2);
            inConversation = true;
            ConversationNumber = 2;
            PositionPlayerInArena2();
            FindObjectOfType<MyGameManager>().audio1.Pause();
            FindObjectOfType<MyGameManager>().audio3.Pause();
            FindObjectOfType<MyGameManager>().audio2.Play();
        }

        if(GameState == 5)
        {
            BlackScreen.GetComponent<Animator>().SetBool("GoBlack", true);
            FindObjectOfType<DialogueManager>().StartDialogue(3);
            inConversation = true;
            ConversationNumber = 3;
            FindObjectOfType<MyGameManager>().audio2.Pause();
            FindObjectOfType<MyGameManager>().audio3.Pause();
            FindObjectOfType<MyGameManager>().audio1.Play();
        }
    }

    public void PositionPlayerInArena1()
    {
        FindObjectOfType<MyGameManager>().audio1.Pause();
        FindObjectOfType<MyGameManager>().audio3.UnPause();
        FindObjectOfType<MyGameManager>().audio2.Pause();

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
