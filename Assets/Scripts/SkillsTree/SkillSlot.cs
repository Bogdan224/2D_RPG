using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillSlot : MonoBehaviour
{
    public int currentLevel;
    public bool isUnlocked;

    public List<SkillSlot> prerequisiteSkillSlots;

    public SkillSO skillSO;
    public Image skillIcon;
    public TMP_Text skillLevelText;

    public Button skillButton;

    public static event Action<SkillSlot> OnAbilityPointSpent;
    public static event Action<SkillSlot> OnSkillMaxed;

    private void OnValidate()
    {
        if(skillSO != null && skillLevelText != null)
        {
            UpdateUI();
        }
    }

    public void TryUpgradeSkill()
    {
        if (isUnlocked && currentLevel < skillSO.maxLevel)
        {
            currentLevel++;
            OnAbilityPointSpent?.Invoke(this);

            if(currentLevel >= skillSO.maxLevel)
            {
                OnSkillMaxed?.Invoke(this);
            }

            UpdateUI();
        }
    }

    public void Unlock()
    {
        isUnlocked = true;
        UpdateUI();
    }

    public bool CanUnlockSkill()
    {
        foreach (var slot in prerequisiteSkillSlots)
        {
            if(!slot.isUnlocked || slot.currentLevel < slot.skillSO.maxLevel)
            {
                return false;
            }
        }
        return true;
    }

    private void UpdateUI()
    {
        skillIcon.sprite = skillSO.skillIcon;

        if (isUnlocked)
        {
            skillButton.interactable = true;
            skillLevelText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();
            skillIcon.color = Color.white;
        }
        else
        {
            skillButton.interactable = false;
            skillLevelText.text = "Locked";
            skillIcon.color = Color.gray;
        }
    }


}
