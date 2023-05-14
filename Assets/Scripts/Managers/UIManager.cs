using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryUI;

    public bool uiIsReady;

    public bool uiActive;
    public bool inventoryActive;
    public bool optionsActive;    

    public void Inventory()
    {
        uiIsReady = false;
        uiActive = !uiActive;
        inventoryActive = !inventoryActive;

        inventoryUI.SetActive(inventoryActive);
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
