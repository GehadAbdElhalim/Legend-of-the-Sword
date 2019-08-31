using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRangedEnemies : MonoBehaviour
{
    public GameObject rangedEnemy;
    public Transform[] SpawnPositionsRanged;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < SpawnPositionsRanged.Length; i++)
            {
                GameObject go = Instantiate(rangedEnemy, SpawnPositionsRanged[i].position, Quaternion.identity);
                go.transform.LookAt(new Vector3(other.transform.position.x, 0, other.transform.position.z));
                go.GetComponent<RangedEnemy>().target = other.gameObject;
            }
            this.GetComponent<Collider>().enabled = false;

            FindObjectOfType<MyGameManager>().audio1.Pause();
            FindObjectOfType<MyGameManager>().audio3.UnPause();
        }
    }
}
