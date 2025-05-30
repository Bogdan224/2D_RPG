using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int expReward = 3;

    public delegate void EnemyDefeated(int exp);
    public static event EnemyDefeated OnEnemyDefeated;

    public int currentHealth;
    public int maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnEnemyDefeated(expReward);
        Destroy(gameObject);
    }
}
