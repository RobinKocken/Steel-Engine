using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public enum ItemType
    {
        none,
        // Materials //
        wood,
        stone, 
        metal,
    }
    public ItemType itemType;

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
    public Vector3 offset;
    public Vector3 stuff;

    void Start()
    {
        InitializeInventory();
        SetSlotID();
    }

    void Update()
    {

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
                    if(currentSlot.item.itemType == item.itemType)
                    {
                        if(itemAmount <= currentSlot.item.maxStack - currentSlot.amount)
                        {
                            currentSlot.amount += itemAmount;
                            return;
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
                    currentSlot.item = item;
                    currentSlot.amount = itemAmount;
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
                    if(currentSlot.item.itemType == item.itemType)
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
}
