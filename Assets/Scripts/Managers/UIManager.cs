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

    void Start()
    {
        InitializeUI();
    }

    void InitializeUI()
    {
        uiIsReady = true;
    }

    public void Inventory()
    {
        if(!uiActive || uiActive && inventoryActive)
        {
            uiIsReady = false;
            uiActive = !uiActive;
            inventoryActive = !inventoryActive;

            inventoryUI.SetActive(inventoryActive);
        }
    }

    public void Build()
    {
        if(!uiActive || uiActive && buildActive)
        {
            uiIsReady = false;
            uiActive = !uiActive;
            buildActive = !buildActive;

            buildUI.SetActive(buildActive);
        }
    }

    public void OptionsUI()
    {

    }

    public void ResetUIInput(int inventoryInput, int buildInput)
    {
        if(!uiIsReady)
        {
            if(inventoryInput == 0 && inventoryActive && !buildActive || buildInput == 0 && buildActive && !inventoryActive || inventoryInput == 0 && buildInput == 0 && !uiIsReady)
            {
                uiIsReady = true;
            }
        }
    }
}
