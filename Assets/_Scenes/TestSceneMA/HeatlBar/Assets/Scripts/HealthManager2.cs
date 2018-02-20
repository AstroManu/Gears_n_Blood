using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager2 : MonoBehaviour
{

    public float maxHealth = 100f;
    public float health;
    public float maxArmor = 0f;
    public float armor = 0f;
    public float maxShield = 0f;
    public float shield = 0f;

    public float dmgReducHealth = 0f;
    public float dmgReducArmor = 0f;
    public float dmgReducShield = 0f;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float amount)

    {
        health -= amount; healthBar.fillAmount = health / maxHealth;
    }


}
