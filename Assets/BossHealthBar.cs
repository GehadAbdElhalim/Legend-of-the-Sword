using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthBar;
    public GameObject go;

    private void Update()
    {
        healthBar.value = go.GetComponent<BossHealth>().currentHealth / go.GetComponent<BossHealth>().Health;
    }
}
