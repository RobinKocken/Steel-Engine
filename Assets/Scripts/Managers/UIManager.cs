using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject ui;
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

            ui.SetActive(uiActive);
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

            ui.SetActive(uiActive);
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
            if(inventoryInput == 0 && buildInput == 0 && !uiIsReady)
            {
                uiIsReady = true;
            }
        }
    }
}
