using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FarmManager : MonoBehaviour
{
    public Button button;
    private FarmController currentFarm;
    public GameObject farmUI;
    public GameObject progressUI;
    public GameObject cropListUI;
    public GameObject harvestUI;

    public void OpenFarmUI(FarmController _farmToGrowOn)
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        currentFarm = _farmToGrowOn;
        //check if a crop is already being grown
        if (currentFarm.currentCrop == null)
        {
            cropListUI.SetActive(true);
            //if no crop is being grown, open a menu with different crop choices
        }
        else
        {
            //if a crop is already being grown, show crop progress
            if (currentFarm.fullyGrown)
            {
                //toggle Harvest UI
            }
            else
            {
                progressUI.GetComponentInChildren<Slider>().value = currentFarm.cropProgress;
                cropListUI.SetActive(false);
                progressUI.SetActive(true);
            }
        }
    }

    public void SelectCrop(int cropIndex)
    {
        currentFarm.PlantCrop(cropIndex);
    }
    public void Harvest()
    {
        currentFarm.HarvestCrops();
    }
}
