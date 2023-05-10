using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    public InventoryManager.ItemType itemType;

    public Sprite icon;
    public int maxStack;
    public GameObject prefab;
}
