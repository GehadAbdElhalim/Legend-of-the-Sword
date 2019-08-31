using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeleeEnemies : MonoBehaviour
{
    public GameObject meleeEnemy;
    public Transform[] SpawnPositionsMelee;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            for (int i = 0; i < SpawnPositionsMelee.Length; i++)
            {
                GameObject go = Instantiate(meleeEnemy,SpawnPositionsMelee[i].position,Quaternion.identity);
                go.transform.LookAt(new Vector3(other.transform.position.x,0, other.transform.position.z));
                go.GetComponent<MeleeEnemy>().target = other.gameObject;
            }
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
