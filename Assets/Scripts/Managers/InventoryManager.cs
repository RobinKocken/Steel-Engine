using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public List<Item> itemHolders;
    public Slot selectedSlot;

    // Parent Holder of the Slots //
    [Header("Slot Holder")]
    public Transform inventorySlotHolder;
    public Transform hotbarSlotHolder;
    public Transform playerHotbarSlotHolder;

    // List for the Inventory Slots //
    [Header("Slots")]
    public List<Transform> slots;
    public List<Transform> inventorySlots;
    public List<Transform> hotbarSlots;
    public List<Transform> playerHotbarSlots;

    [Header("Mouse")]
    public Transform cursor;
    public Image iconHolder;
    public TMP_Text amountTextHolder;
    public Vector3 offset;
    public Item itemHolder;
    public int amountHolder;
    public bool cursorActive;

    [Header("Scrollwheel")]
    public float scrollWheel;
    float oldScroll = 1;
    public Color defaultColor;
    public Color selectedColor;

    void Start()
    {
        InitializeInventory();
        SetSlotID();
        SyncHotBar();
    }
    
    public void PlayerUpdate()
    {
        Scrollbar();
    }

    public void InventoryUpdate()
    {
        MouseItemTracking();
        InteractWithSelectedItem();
    }

    void MouseItemTracking()
    {
        if(cursorActive)
        {
            cursor.position = Input.mousePosition + offset;
        }
    }

    void Scrollbar()
    {
        scrollWheel += Input.mouseScrollDelta.y;
        scrollWheel = Mathf.Clamp(scrollWheel, -playerHotbarSlots.Count + 1, 0);

        if(oldScroll != scrollWheel)
        {
            playerHotbarSlots[Mathf.Abs((int)oldScroll)].GetComponent<Image>().color = defaultColor;
            playerHotbarSlots[Mathf.Abs((int)scrollWheel)].GetComponent<Image>().color = selectedColor;
            selectedSlot = hotbarSlots[Mathf.Abs((int)scrollWheel)].GetComponent<Slot>();

            oldScroll = scrollWheel;
        }
    }

    void InteractWithSelectedItem()
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

        for(int i = 0; i < playerHotbarSlotHolder.childCount; i++)
        {
            playerHotbarSlots.Add(playerHotbarSlotHolder.GetChild(i));
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

        for(int i = 0; i < playerHotbarSlots.Count; i++)
        {
            if(playerHotbarSlots[i].TryGetComponent<Slot>(out Slot currentSlot) && hotbarSlots[i].TryGetComponent<Slot>(out Slot currentHotBarSlot))
            {
                currentSlot.slotID = currentHotBarSlot.slotID;
            }
        }
    }

    // Get the total Amount of an Item in the whole Inventory //
    public int GetTotalAmount(Item item)
    {
        int totalAmount = 0;

        for(int i = 0; i < slots.Count; i++)
        {
            if(slots[i].TryGetComponent<Slot>(out Slot currentSlot))
            {
                if(currentSlot.item.name == item.name)
                {
                    totalAmount += currentSlot.amount;
                }
            }
        }

        return totalAmount;
    }

    // Add Funtion If Inventory is Full //
    public void AddItem(Item item, int itemAmount, int slotID)
    { 
        if(slotID > -1)
        {
            if(slots[slotID].TryGetComponent<Slot>(out Slot currentSlot))
            {
                currentSlot.SetItem(item, itemAmount);
                return;
            }
        }

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

        SyncHotBar();
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

        SyncHotBar();
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

        SyncHotBar();
    }

    void SyncHotBar()
    {
        for(int i = 0; i < hotbarSlots.Count; i++)
        {
            if(playerHotbarSlots[i].TryGetComponent<Slot>(out Slot currentSlot) && hotbarSlots[i].TryGetComponent<Slot>(out Slot currentHotBarSlot))
            {
                if(currentSlot.amount == 0 && currentHotBarSlot.amount != 0)
                    currentSlot.SetItem(currentHotBarSlot.item, currentHotBarSlot.amount);
                else if(currentSlot.amount != 0 && currentHotBarSlot.amount != 0)
                    currentSlot.amount = currentHotBarSlot.amount;
                else if(currentSlot.amount != 0 && currentHotBarSlot.amount == 0)
                    currentSlot.DeleteItem();
            }
        }
    }

    void Cursor()
    {
        cursorActive = !cursorActive;
        iconHolder.sprite = itemHolder.icon;
        cursor.gameObject.SetActive(cursorActive);
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
