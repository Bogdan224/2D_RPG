using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ExpManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToLevel = 10;
    public float expGrowthMultiplier = 1.2f;
    public Slider expSlider;
    public TMP_Text currentLevelText;

    public static event Action<int> OnLevelUp;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
              
    }

    public void GainExperience(int amount)
    {
        currentExp += amount;
        if(currentExp >= expToLevel)
        {
            LevelUp();
        }
        UpdateUI();
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyDefeated += GainExperience;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyDefeated -= GainExperience;
    }

    private void LevelUp()
    {
        do
        {
            level++;
            currentExp -= expToLevel;
            expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultiplier);
            OnLevelUp?.Invoke(1);
        }
        while (currentExp >= expToLevel);
        
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLevelText.text = "Level: " + level;
    }
}
