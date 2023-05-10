using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public InventoryManager.ItemType itemType;

    public int slotID;

    public Item item;
    public int amount;

    public Image iconRenderer;
    public TMP_Text currentAmountText;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
