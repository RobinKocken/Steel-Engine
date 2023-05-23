using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crop", menuName = "CropData", order = 2)]
public class CropData : ScriptableObject
{
    public InventoryManager.ItemName type;
    public GameObject[] GrowthStages;
    public int timeToGrow;
    
}
