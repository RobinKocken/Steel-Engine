using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crop", menuName = "Crop", order = 2)]
public class CropData : ScriptableObject
{
    public string name;
    public int GrowthStage;
    public GameObject[] GrowthStages;
    public int timeToGrow;
    private GameObject oldCrop;

    public void Grow()
    {
        if (oldCrop != null)
        {
            Destroy(oldCrop);
        }
        oldCrop = Instantiate(GrowthStages[GrowthStage]);
    }
}
