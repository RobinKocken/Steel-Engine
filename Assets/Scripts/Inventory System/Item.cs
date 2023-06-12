using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    public int itemID;
    public string itemName;
    public int maxStack;

    public Sprite icon;

    public GameObject prefab;
}
