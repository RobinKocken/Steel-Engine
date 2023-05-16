using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmController : MonoBehaviour
{
    [SerializeField] private Crop[] crops;
    public Crop currentCrop;
    private int cropToGrow;
    public void OpenFarmUI()
    {
        //check if a crop is already being grown
        if (currentCrop == null)
        {
            //if no crop is being grown, open a menu with different crop choices
        }
        else
        {
            //if a crop is already being grown, show crop progress
        }
    }

    public void PlantCrop(int _cropToGrow)
    {
        cropToGrow = _cropToGrow;
        GrowCrop();
    }
    private void GrowCrop()
    {
        currentCrop = crops[cropToGrow];
        StartCoroutine(GrowCrop(crops[cropToGrow]));
    }

    public IEnumerator GrowCrop(Crop _cropToGrow)
    {
        yield return new WaitForSeconds(_cropToGrow.timeToGrow / 4);
        
        if (_cropToGrow.GrowthStage == 3)
        {
            StopCoroutine(GrowCrop(null));
        }

    }

    public void HarvestCrops()
    {
        //harvest the crop and put it in the inventory
    }

}
