using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour, IInteractable
{
    public InventoryManager inventoryManager;

    public Item item;
    public int currentAmount;

    public void Interact()
    {
        PickUp();
    }

    void PickUp()
    {
        inventoryManager.AddItem(item, currentAmount);
        Destroy(gameObject);
    }
}
