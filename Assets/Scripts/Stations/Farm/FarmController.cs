using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FarmController : MonoBehaviour
{
    [SerializeField] private CropData[] crops;
    public CropData currentCrop;
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

    public IEnumerator GrowCrop(CropData _cropToGrow)
    {
        if (_cropToGrow.GrowthStage == _cropToGrow.GrowthStages.Length)
        {
            StopCoroutine(GrowCrop(null));
        }
        yield return new WaitForSeconds(_cropToGrow.timeToGrow / 4);

        _cropToGrow.GrowthStage++;
        _cropToGrow.GetComponent<CropData>().Grow();

    }

    public void HarvestCrops()
    {
        //harvest the crop and put it in the inventory
    }

}
