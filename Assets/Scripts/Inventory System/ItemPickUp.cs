using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour, IInteractable
{
    public Item item;
    public int currentAmount;
    public bool destroyThisObject;

    public void Interact(GameManager gameManager)
    {
        PickUp(gameManager);
    }

    void PickUp(GameManager gameManager)
    {
        gameManager.inventoryManager.AddItem(item, currentAmount, -1);

        item = null;
        currentAmount = 0;

        if(destroyThisObject)
            Destroy(gameObject);
    }
}
