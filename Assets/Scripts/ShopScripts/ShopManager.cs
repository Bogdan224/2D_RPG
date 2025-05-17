using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{   
    [SerializeField]
    private ShopSlot[] shopSlots;

    [SerializeField]
    private InventoryManager inventoryManager;

    public void PopulateShopItems(List<ShopItem> shopItems)
    {
        for (int i = 0; i < shopItems.Count && i < shopSlots.Length; i++)
        {
            ShopItem shopItem = shopItems[i];
            shopSlots[i].Initialize(shopItem.itemSO, shopItem.price);
            shopSlots[i].gameObject.SetActive(true);
        }

        for (int i = shopItems.Count; i < shopSlots.Length; i++)
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    }

    public void TryBuyItem(ItemSO itemSO, int price, int quantity = 1)
    {
        if(itemSO != null && inventoryManager.goldQuantity >= price * quantity)
        {
            if (HasSpaceForItem(itemSO))
            {
                inventoryManager.goldQuantity -= price * quantity;
                inventoryManager.goldText.text = inventoryManager.goldQuantity.ToString();
                inventoryManager.AddItem(itemSO, quantity);
            }
        }
    }

    private bool HasSpaceForItem(ItemSO itemSO)
    {
        foreach (var slot in inventoryManager.inventorySlots)
        {
            if (slot.itemSO == itemSO && slot.quantity < itemSO.stackSize)
                return true;
            else if (slot.itemSO == null)
                return true;
        }
        return false;
    }

    public void SellItem(ItemSO itemSO)
    {
        if (itemSO == null)
            return;

        foreach (var slot in shopSlots)
        {
            if(slot.itemSO == itemSO)
            {
                inventoryManager.goldQuantity += slot.price;
                inventoryManager.goldText.text = inventoryManager.goldQuantity.ToString();
                return;
            }
        }
    }

}


[System.Serializable]
public class ShopItem
{
    public ItemSO itemSO;
    public int price;
}

