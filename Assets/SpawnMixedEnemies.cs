using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMixedEnemies : MonoBehaviour
{
    public GameObject rangedEnemy;
    public GameObject meleeEnemy;
    public Transform[] SpawnPositionsMixed;
    bool melee = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < SpawnPositionsMixed.Length; i++)
            {
                if (!melee)
                {
                    GameObject go = Instantiate(rangedEnemy, SpawnPositionsMixed[i].position, Quaternion.identity);
                    go.transform.LookAt(new Vector3(other.transform.position.x, 0, other.transform.position.z));
                    go.GetComponent<RangedEnemy>().target = other.gameObject;
                }
                else
                {
                    GameObject go = Instantiate(meleeEnemy, SpawnPositionsMixed[i].position, Quaternion.identity);
                    go.transform.LookAt(new Vector3(other.transform.position.x, 0, other.transform.position.z));
                    go.GetComponent<MeleeEnemy>().target = other.gameObject;
                }
                melee = !melee;
            }
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
