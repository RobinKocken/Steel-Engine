using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public Item item;
    public int amount;

    public int slotID;

    public Image iconRenderer;
    public TMP_Text currentAmountText;

    public int AddAmountToItem
    {
        set
        {
            amount += value;
        }
    }

    public int SubAmountFromItem
    {
        set
        {
            amount -= value;
        }        
    }

    public void SelectButton()
    {
        inventoryManager.PickUpDropItems(slotID);
    }

    public void SelectSlot()
    {
        inventoryManager.PickUpDropItems(slotID);
    }

    public void SetItem(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void DeleteItem()
    {
        item = null;
        amount = 0;
    }
}
