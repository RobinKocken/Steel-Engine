using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject buildUI;

    public bool uiIsReady;

    public bool uiActive;

    public bool inventoryActive;
    public bool buildActive;
    public bool optionsActive;    

    public void Inventory()
    {
        if(!uiActive)
        {
            uiIsReady = false;
            uiActive = !uiActive;
            //inventoryActive = !inventoryActive;

            inventoryUI.SetActive(inventoryActive);
        }
    }

    public void Build()
    {
        if(!uiActive)
        {
            uiIsReady = false;
            uiActive = !uiActive;
            //buildActive = !buildActive;

            buildUI.SetActive(buildActive);
        }
    }

    public void OptionsUI()
    {

    }

    public void ResetUIInput(int input)
    {
        if(input == 0)
        {
            uiIsReady = true;
        }
    }
}
