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
            slots.Add(inventorySlotHolder.GetChild(i));
            hotbarSlots.Add(inventorySlotHolder.GetChild(i));
        }
    }

    void SetSlotID()
    {
        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].GetComponent<Slot>() != null)
            {
                // Sets an ID for every Slot in the Inventory //
                slots[i].GetComponent<Slot>().slotID = i;
            }
        }
    }
}
