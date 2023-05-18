using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public Keys keys;
    public InventoryManager inventoryManager;

    public enum InternalUIState
    {
        none,
        quest,
        inventory,
        map,
        option,
    }
    public InternalUIState internalUIState;

    public GameObject uiInternal;
    public GameObject uiQuest;
    public GameObject uiInventory;
    public GameObject uiMap;
    public GameObject uiOption;

    public enum ExternalUIState
    {
        none,
        build,
        craft,
        farm,
    }
    public ExternalUIState externalUIState;

    public GameObject uiExternal;
    public GameObject uiBuild;
    public GameObject uiCraft;
    public GameObject uiFarm;

    void Start()
    {
        StateUI(InternalUIState.none, ExternalUIState.none);
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

    public void InternalUIUpdate(KeyCode inventoryKey)
    {
        if(Input.GetKeyDown(inventoryKey))
        {
            gameManager.SwitchStatePlayer(GameManager.PlayerState.player);
        }

        inventoryManager.InventoryUpdate();
    }

    public void StateUI(InternalUIState iInternalUIState, ExternalUIState eExternalUIState)
    {
        internalUIState = iInternalUIState;
        externalUIState = eExternalUIState;

        if(internalUIState != InternalUIState.none)
        {
            switch(internalUIState)
            {
                case InternalUIState.quest:
                {
                    Internal(true, false, false, false);
                    break;
                }
                case InternalUIState.inventory:
                {
                    Internal(false, true, false, false);
                    break;
                }
                case InternalUIState.map:
                {
                    Internal(false, false, true, false);
                    break;
                }
                case InternalUIState.option:
                {
                    Internal(false, false, false, true);
                    break;
                }
            }
        }
        else if(externalUIState != ExternalUIState.none)
        {
            switch(externalUIState)
            {
                case ExternalUIState.build:
                {
                    External(true, false, false);
                    break;
                }
                case ExternalUIState.craft:
                {
                    External(false, true, false);
                    break;
                }
                case ExternalUIState.farm:
                {
                    External(false, false, true);
                    break;
                }
            }
        }
        else
        {
            Internal(false, false, false, false);
            External(false, false, false);
        }
    }

    void Internal(bool questActive, bool inventoryActive, bool mapActive, bool optionActive)
    {
        if(questActive || inventoryActive || mapActive || optionActive)
            uiInternal.SetActive(true);
        else
            uiInternal.SetActive(false);

        uiQuest.SetActive(questActive);
        uiInventory.SetActive(inventoryActive);
        uiMap.SetActive(mapActive);
        uiOption.SetActive(optionActive);
    }

    void External(bool builActive, bool craftActive, bool farmActive)
    {
        if(builActive || craftActive || farmActive)
            uiExternal.SetActive(true);
        else
            uiExternal.SetActive(false);

        uiBuild.SetActive(builActive);
        uiCraft.SetActive(craftActive);
        uiFarm.SetActive(farmActive);
    }
}
