using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;

    public TMP_Text healthText;
    public Animator healthTextAnim;

    private void Start()
    {
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = "HP: " + CurrentHealth + "/" + MaxHealth;
    }

    public void ChangeHealth(int amount)
    {
        CurrentHealth += amount;
        healthTextAnim.Play("TextUpdate");
        
        if(CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            gameObject.SetActive(false);
        }
        UpdateHealthText();
    }
}
