using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour, IInteractable
{
    public Item item;
    public int currentAmount;
    public bool destroyThisObject;

    public void Interact(InventoryManager inventoryManager)
    {
        if(inventoryManager != null)
            PickUp(inventoryManager);
    }

    void PickUp(InventoryManager inventoryManager)
    {
        inventoryManager.AddItem(item, currentAmount);

        item = null;
        currentAmount = 0;

        if(destroyThisObject)
            Destroy(gameObject);
    }
}
