using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Keys keys;

    // Possible States for the Player //
    public enum PlayerState
    {
        player,
        station,
        ui,
        build,
    }
    public PlayerState playerState;

    public PlayerController playerController;
    public RaycastController raycastController;

    public OptionManager optionManager;
    public UIManager uiManager;
    public InventoryManager inventoryManager;
    public BuildManager buildManager;

    public GameObject playerCamera;
    public GameObject buildCamera;

    void Awake()
    {
        
    }

    void Start()
    {
        SwitchStatePlayer(PlayerState.player);
    }

    void Update()
    {
        StatePlayer();
    }

    // State of the Player //
    void StatePlayer()
    {
        switch(playerState)
        {
            case PlayerState.player:
            {
                InputForSwitchStatePlayer();
                playerController.GetPlayerKeyInput(keys.forwardKey, keys.backwardsKey, keys.leftKey, keys.rightKey, keys.runKey, keys.jumpKey);
                raycastController.GetInteractionKeyInput(keys.interactionKey);
                break;
            }
            case PlayerState.station:
            {
                break;
            }
            case PlayerState.ui:
            {
                if(uiManager.internalUIState != UIManager.InternalUIState.none)
                {
                    uiManager.InternalUIUpdate(keys.menuKey);
                    break;
                }
                else if(uiManager.externalUIState != UIManager.ExternalUIState.none)
                {
                    uiManager.ExternalUIUpdate(keys.interactionKey);
                    break;
                }

                break;
            }
            case PlayerState.build:
            {
                InputForSwitchStatePlayer();
                // Delete Later //
                if(buildManager != null)
                {
                    buildManager.BuildInput();
                }
                break;
            }
        }
    }

    // Make from this a cleaner Version //
    public void SwitchStatePlayer(PlayerState pPlayerState)
    {
        playerState = pPlayerState;

        switch(playerState)
        {
            case PlayerState.player:
            {
                CursorModeLocked();
                SwitchCamera(playerCamera, buildCamera);
                uiManager.StateUI(UIManager.InternalUIState.none, UIManager.ExternalUIState.none);
                break;
            }
            case PlayerState.station:
            {
                CursorModeLocked();
                playerController.StopMovement();
                break;
            }
            case PlayerState.ui:
            {
                CursorModeConfined();
                playerController.StopMovement();

                if(Input.GetKeyDown(keys.menuKey))
                {
                    uiManager.StateUI(UIManager.InternalUIState.inventory, UIManager.ExternalUIState.none);
                }
                else if(Input.GetKeyDown(keys.interactionKey))
                {
                    uiManager.StateUI(UIManager.InternalUIState.none, UIManager.ExternalUIState.farm); ;
                }

                break;
            }
            case PlayerState.build:
            {
                CursorModeConfined();
                playerController.StopMovement();
                SwitchCamera(buildCamera, playerCamera);
                uiManager.StateUI(UIManager.InternalUIState.none, UIManager.ExternalUIState.build);
                break;
            }
        }
    }

    void InputForSwitchStatePlayer()
    {
        if(Input.GetKeyDown(keys.journalKey) || Input.GetKeyDown(keys.menuKey) || Input.GetKeyDown(keys.mapKey) || Input.GetKeyDown(keys.optionKey))
        {
            if(playerState == PlayerState.player)
                SwitchStatePlayer(PlayerState.ui);
        }

        if(Input.GetKeyDown(keys.buildKey))
        {
            if(playerState == PlayerState.player)
                SwitchStatePlayer(PlayerState.build);
            else if(playerState == PlayerState.build)
                SwitchStatePlayer(PlayerState.player);
        }       
    }

    void SwitchCamera(GameObject camEnable, GameObject camDisable)
    {
        camEnable.SetActive(true);
        camDisable.SetActive(false);
    }

    void CursorModeLocked()
    { 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        OptionManager.playerMouseSens = optionManager.mouseSens;
    }

    void CursorModeConfined()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        
        OptionManager.playerMouseSens = 0;
    }

    public bool CheckIfKeyCodeIsTrue(KeyCode key)
    {
        foreach(KeyCode kCode in Enum.GetValues(typeof(KeyCode)))
        {
            if(kCode == key)
            {
                Debug.Log($"{kCode}");
                return true;
            }
        }
        return false;
    }
}

[System.Serializable]
public class Keys
{
    [Header("Player Keys")]
    public KeyCode forwardKey;
    public KeyCode backwardsKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode runKey;
    public KeyCode jumpKey;

    [Header("UI Keys")]
    public KeyCode journalKey;
    public KeyCode menuKey;
    public KeyCode mapKey;
    public KeyCode optionKey;

    [Header("Interaction Key")]
    public KeyCode interactionKey;

    [Header("Build Mode Key")]
    public KeyCode buildKey;
}
