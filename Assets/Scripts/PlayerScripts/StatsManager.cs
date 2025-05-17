using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public StatsUI statsUI;
    public TMP_Text healthText;

    [Header("Combat Stats")]
    public int damage = 1;
    public float WeaponRange;
    public float KnockbackForce;
    public float StunTime;

    [Header("Movement Stats")]
    public float speed = 5;

    [Header("Health Stats")]
    public int MaxHealth;
    public int CurrentHealth;

    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
            statsUI.UpdateDamage();
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
            statsUI.UpdateSpeed();
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateMaxHealth(int amount)
    {
        MaxHealth += amount;
        healthText.text = "HP: " + CurrentHealth + "/" + MaxHealth;
    }

    public void UpdateCurrentHealth(int amount)
    {
        CurrentHealth += amount;
        if(CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
        healthText.text = "HP: " + CurrentHealth + "/" + MaxHealth;
    }

}
