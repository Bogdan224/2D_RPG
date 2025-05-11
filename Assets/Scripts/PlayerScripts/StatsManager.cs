using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;

    [Header("Combat Stats")]
    public int Damage;
    public float WeaponRange;
    public float KnockbackForce;
    public float StunTime;

    [Header("Movement Stats")]
    public float Speed;

    [Header("Health Stats")]
    public float MaxHealth;
    public float CurrentHealth;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
