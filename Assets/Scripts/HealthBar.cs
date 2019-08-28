using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public GameObject go;

    private void Update()
    {
        healthBar.value = go.GetComponent<MeleeEnemyHealthSystem>().currentHealth/go.GetComponent<MeleeEnemyHealthSystem>().Health;
    }
}
