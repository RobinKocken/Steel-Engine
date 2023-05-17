using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public enum UIState
    {
        quests,
        inventory,
        map,
        option,
    }
    public UIState uiState;

    public GameObject ui;
    public GameObject inventoryUI;
    public GameObject buildUI;

    public bool uiActive;
    public bool inventoryActive;
    public bool buildActive;
    public bool optionsActive;

    void Start()
    {
        InitializeUI();
    }

    public TMP_Text fpsCounter;
    float fps;
    float updateTimer = 0.2f;

    void Update()
    {
        updateTimer -= Time.deltaTime;
        if(updateTimer <= 0)
        {
            fps = 1 / Time.unscaledDeltaTime;
            fpsCounter.text = $"FPS: {Mathf.Round(fps)}";
            updateTimer = 0.2f;
        }
    }

    public void StateUI()
    {
        switch(uiState)
        {
            case UIState.quests:
            {
                break;
            }
            case UIState.inventory:
            {
                inventoryManager.InventoryUpdate();
                break;
            }
            case UIState.map:
            {
                break;
            }
            case UIState.option:
            {
                break;
            }
        }
    }

    void SwitchStateUI()
    {

        StateUI();
    }

    public void GetUIInput()
    {

    }

    void InitializeUI()
    {
        
    }

    public void Inventory(bool active)
    {
        if(!uiActive || uiActive && inventoryActive)
        {
            uiActive = active;
            inventoryActive = active;

            ui.SetActive(active);
            inventoryUI.SetActive(active);
        }
    }

    public void Build(bool active)
    {
        if(!uiActive || uiActive && buildActive)
        {
            uiActive = active;
            buildActive = active;

            ui.SetActive(active);
            buildUI.SetActive(active);
        }
    }

    public void OptionsUI()
    {

    }

    public void ChangeUIStateButton(UIState uiState)
    {
        
    }
}
