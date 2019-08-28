using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider CooldownBar;
    public Slider healthBar;
    public GameObject go;

    private void Update()
    {
        healthBar.value = go.GetComponent<PlayerHealthSystem>().currentHealth / go.GetComponent<PlayerHealthSystem>().Health;
        CooldownBar.value = go.GetComponent<Movement>().currentCooldown / go.GetComponent<Movement>().cooldown;
    }
}
