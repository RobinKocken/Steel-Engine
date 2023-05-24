using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FarmManager : MonoBehaviour
{
    public UnityEngine.UIElements.Button button;
    private FarmController currentFarm;
    public GameObject farmUI;
    public GameObject progressUI;
    public UnityEngine.UI.Slider progressSlider;
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
                progressSlider.value = progressSlider.maxValue;
            }
            else
            {
                cropListUI.SetActive(false);
                progressUI.SetActive(true);

                progressSlider.value = currentFarm.cropProgress;
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
