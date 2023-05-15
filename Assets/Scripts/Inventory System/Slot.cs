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
            currentAmountText.text = amount.ToString();
        }
    }

    public int SubAmountFromItem
    {
        set
        {
            amount -= value;
            currentAmountText.text = amount.ToString();
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

        iconRenderer.gameObject.SetActive(true);
        iconRenderer.sprite = item.icon;

        currentAmountText.gameObject.SetActive(true);
        currentAmountText.text = this.amount.ToString();
    }

    public void DeleteItem()
    {
        item = null;
        amount = 0;

        iconRenderer.sprite = null;
        iconRenderer.gameObject.SetActive(false);

        currentAmountText.text = "0";
        currentAmountText.gameObject.SetActive(false);
    }
}
