using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image CooldownBar;
    public Image healthBar;
    public GameObject go;

    private void Update()
    {
        healthBar.fillAmount = go.GetComponent<PlayerHealthSystem>().currentHealth / go.GetComponent<PlayerHealthSystem>().Health;
        CooldownBar.fillAmount = go.GetComponent<Movement>().cooldown - go.GetComponent<Movement>().currentCooldown / go.GetComponent<Movement>().cooldown;
    }
}
