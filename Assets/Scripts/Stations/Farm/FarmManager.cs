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

    public void OpenFarmUI(FarmController _farmToGrowOn)
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        currentFarm = _farmToGrowOn;
        //check if a crop is already being grown
        if (_farmToGrowOn.currentCrop == null)
        {
           cropListUI.SetActive(true);
            //if no crop is being grown, open a menu with different crop choices
        }
        else
        {
            cropListUI.SetActive(false);
            progressUI.SetActive(true);
            //if a crop is already being grown, show crop progress
        }
    }

    public void SelecCrop(int cropIndex)
    {
        currentFarm.PlantCrop(cropIndex);
    }
}
