using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public int GrowthStage;
    private GameObject oldCrop;
    public CropData cropData;

    public void Start()
    {
        
    }
    public void Grow()
    {
        if (oldCrop != null)
        {
            Destroy(oldCrop);
        }
        oldCrop = Instantiate(cropData.GrowthStages[GrowthStage]);
    }
}
