using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    public static int GameState = 0;
    public GameObject Player;
    public GameObject MeleeEnemy;
    
    public Transform[] SpawnPositionsMelee;
    public Transform[] SpawnPositionsRanged;
    public Transform[] SpawnPositionsMixed;

    public void StratGameState(int State)
    {
        if (State == 0)
        {

        }
    }
}
