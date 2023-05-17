using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public enum ItemName
    {
        none,
        wood,
        stone,
        metal,
        corn,
    }
    public ItemName itemName;

    // Parent Holder of the Slots //
    [Header("Slot Holder")]
    public Transform inventorySlotHolder;
    public Transform hotbarSlotHolder;

    // List for the Inventory Slots //
    [Header("Slots")]
    public List<Transform> slots;
    public List<Transform> inventorySlots;
    public List<Transform> hotbarSlots;

    [Header("Mouse")]
    public Transform cursor;
    public Image iconHolder;
    public TMP_Text amountTextHolder;
    public Vector3 offset;
    public Item itemHolder;
    public int amountHolder;
    public bool cursorActive;


    void Start()
    {
        InitializeInventory();
        SetSlotID();
    }

    public void InventoryUpdate()
    {
        MouseItemTracking();
    }

    void MouseItemTracking()
    {
        if(cursorActive)
        {
            cursor.position = Input.mousePosition + offset;
        }
    }

    void Cursor()
    {
        cursorActive = !cursorActive;
        iconHolder.sprite = itemHolder.icon;
        cursor.gameObject.SetActive(cursorActive);
    }

    void InitializeInventory()
    {
        // Sets Inventory Slots in Inventory List //
        for(int i = 0; i < inventorySlotHolder.childCount; i++)
        {
            slots.Add(inventorySlotHolder.GetChild(i));
            inventorySlots.Add(inventorySlotHolder.GetChild(i));
        }

        // Sets Hotbar Slots in Inventory and Hotbar List //
        for(int i = 0; i < hotbarSlotHolder.childCount; i++)
        {
            slots.Add(hotbarSlotHolder.GetChild(i));
            hotbarSlots.Add(hotbarSlotHolder.GetChild(i));
        }
    }

    void SetSlotID()
    {
        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].TryGetComponent<Slot>(out Slot currentSlot))
            {
                if(currentSlot != null)
                {
                    // Sets an ID for every Slot in the Inventory //
                    currentSlot.slotID = i;
                }
            }
        }
    }

    // Add Funtion If Inventory is Full //
    public void AddItem(Item item, int itemAmount)
    { 
        // Check if Item can be added to already existing Slot with the same Item //
        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].TryGetComponent<Slot>(out Slot currentSlot))
            {
                if(currentSlot.item != null)
                {
                    if(currentSlot.item.itemName == item.itemName)
                    {
                        // Full Amount is Added to Slot with the Same Item //
                        if(itemAmount <= currentSlot.item.maxStack - currentSlot.amount)
                        {
                            currentSlot.AddAmountToItem = itemAmount;
                            return;
                        }
                        // Partial Amount is Add to Slot because Max Stack is archieved // 
                        else if(itemAmount > currentSlot.item.maxStack - currentSlot.amount)
                        {
                            int maxAmount = currentSlot.item.maxStack - currentSlot.amount;
                            itemAmount -= maxAmount;
                            currentSlot.AddAmountToItem = maxAmount;
                            continue;
                        }
                    }
                }
            }
        }

        // Add Item to Empty Slot //
        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].TryGetComponent<Slot>(out Slot currentSlot))
            {
                if(currentSlot.item == null)
                {
                    currentSlot.SetItem(item, itemAmount);

                    return;
                }
            }
        }
    }

    public void RemoveItem(Item item, int itemAmount)
    {
        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].TryGetComponent<Slot>(out Slot currentSlot))
            {
                if(currentSlot.item != null)
                {
                    if(currentSlot.item.itemName == item.itemName)
                    {
                        if(itemAmount <= currentSlot.amount)
                        {
                            currentSlot.amount -= itemAmount;
                            return;
                        }
                    }
                }
            }
        }
    }

    public void PickUpDropItems(int slotID)
    {
        if(slots[slotID].TryGetComponent<Slot>(out Slot currentSlot))
        {
            // Put Selected Slot Item in Cursor and Empty Slot //
            if(currentSlot.item != null && itemHolder == null)
            {
                SetItemInCursorHolder(currentSlot.item, currentSlot.amount);
                Cursor();
                currentSlot.DeleteItem();
            }
            // Set Item in Selected Slot and Empty Cursor //
            else if(currentSlot.item == null && itemHolder != null)
            {
                currentSlot.SetItem(itemHolder, amountHolder);
                Cursor();
                DeleteItemInCursorHolder();
            }
            // Add an amount to an Item from the same Type //
            else if(currentSlot.item != null && itemHolder != null)
            {
                if(currentSlot.item.itemName == itemHolder.itemName && currentSlot.amount != currentSlot.item.maxStack)
                {
                    // Full Amount is Added to Selected Slot //
                    if(amountHolder <= currentSlot.item.maxStack - currentSlot.amount)
                    {
                        currentSlot.AddAmountToItem = amountHolder;
                        Cursor();
                        DeleteItemInCursorHolder();
                    }
                    // Only the Max Avaible Amount is Added to Selected Slot //                    
                    if(amountHolder > currentSlot.item.maxStack - currentSlot.amount)
                    {
                        int maxAmount = currentSlot.item.maxStack - currentSlot.amount;

                        SubAmountCursor = maxAmount;
                        currentSlot.AddAmountToItem = amountHolder;
                    }
                    
                }
            }
        }
    }

    void SetItemInCursorHolder(Item item, int amount)
    {
        itemHolder = item;
        amountHolder = amount;
    }

    void DeleteItemInCursorHolder()
    {
        itemHolder = null;
        amountHolder = 0;
    }

    int AddAmountCursor
    {
        set
        {
            amountHolder += value;
        }        
    }

    int SubAmountCursor
    {
        set
        {
            amountHolder -= value;
        }
    }
}
