using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public TMP_Text healthText;
    public Animator healthTextAnim;

    private void Start()
    {
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = "HP: " + StatsManager.Instance.CurrentHealth + "/" + StatsManager.Instance.MaxHealth;
    }

    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.CurrentHealth += amount;
        healthTextAnim.Play("TextUpdate");
        
        if(StatsManager.Instance.CurrentHealth <= 0)
        {
            StatsManager.Instance.CurrentHealth = 0;
            gameObject.SetActive(false);
        }
        UpdateHealthText();
    }
}
