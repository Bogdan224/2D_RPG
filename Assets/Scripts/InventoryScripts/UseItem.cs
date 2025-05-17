using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public void ApllyItemEffects(ItemSO itemSO)
    {
        if (itemSO.maxHealth > 0)
            StatsManager.Instance.UpdateMaxHealth(itemSO.maxHealth);
        if (itemSO.currentHealth > 0)
            StatsManager.Instance.UpdateCurrentHealth(itemSO.currentHealth);
        if (itemSO.speed > 0)
            StatsManager.Instance.Speed += itemSO.speed;
        if (itemSO.duration > 0)
            StartCoroutine(EffectTimer(itemSO, itemSO.duration));
    }

    private IEnumerator EffectTimer(ItemSO itemSO, float duration)
    {
        yield return new WaitForSeconds(duration);
        if (itemSO.currentHealth > 0)
            StatsManager.Instance.UpdateCurrentHealth(-itemSO.currentHealth);
        if (itemSO.maxHealth > 0)
            StatsManager.Instance.UpdateMaxHealth(-itemSO.maxHealth);
        if (itemSO.speed > 0)
            StatsManager.Instance.Speed -= itemSO.speed;
    }
}
