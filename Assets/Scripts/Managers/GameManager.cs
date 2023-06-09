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
    public BaseController baseController;

    public OptionManager optionManager;
    public UIManager uiManager;
    public InventoryManager inventoryManager;
    public BuildManager buildManager;

    public GameObject playerCamera;
    public GameObject buildCamera;

    void Awake()
    {
        SaveSystem.instance.gameManager = this;
        if(SaveSystem.instance.Datastate == SaveSystem.SystemState.Loading)
        {
            Debug.Log(SaveSystem.instance.Datastate);
            SaveSystem.instance.LoadData();
        }
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
                playerController.GetPlayerKeyInput(keys.playerForwardKey, keys.playerBackwardsKey, keys.playerLeftKey, keys.playerRightKey, keys.playerRunKey, keys.playerJumpKey);
                raycastController.GetInteractionKeyInput(keys.interactionKey);
                break;
            }
            case PlayerState.station:
            {
                baseController.GetBaseKeyInput(keys.baseForwardKey, keys.baseBackwardsKey, keys.baseLeftKey, keys.baseRightKey, keys.baseSwitchCamKey, keys.interactionKey);

                break;
            }
            case PlayerState.ui:
            {
                if(uiManager.internalUIState != UIManager.InternalUIState.none)
                {
                    uiManager.InternalUIUpdate(keys.journalKey, keys.inventoryKey, keys.mapKey, keys.optionKey);
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

                // Check which UI Button is pressed to Change to the correct UI State //
                if(Input.GetKeyDown(keys.journalKey))
                    uiManager.SwitchStateUI(UIManager.InternalUIState.journal, UIManager.ExternalUIState.none);
                else if(Input.GetKeyDown(keys.inventoryKey))
                    uiManager.SwitchStateUI(UIManager.InternalUIState.inventory, UIManager.ExternalUIState.none);
                else if(Input.GetKeyDown(keys.mapKey))
                    uiManager.SwitchStateUI(UIManager.InternalUIState.map, UIManager.ExternalUIState.none);
                else if(Input.GetKeyDown(keys.optionKey))
                    uiManager.SwitchStateUI(UIManager.InternalUIState.option, UIManager.ExternalUIState.none);
                else if(Input.GetKeyDown(keys.interactionKey))
                    uiManager.SwitchStateUI(UIManager.InternalUIState.none, UIManager.ExternalUIState.farm); 

                break;
            }
            case PlayerState.build:
            {
                CursorModeConfined();
                playerController.StopMovement();
                buildCamera.GetComponent<BuildCam>().targetOffset = buildManager.transform.position;
                SwitchCamera(buildCamera, playerCamera);

                uiManager.SwitchStateUI(UIManager.InternalUIState.none, UIManager.ExternalUIState.build);
                break;
            }
        }
    }

    void InputForSwitchStatePlayer()
    {
        // Check if an UI is pressed and change to UI State //
        if(Input.GetKeyDown(keys.journalKey) || Input.GetKeyDown(keys.inventoryKey) || Input.GetKeyDown(keys.mapKey) || Input.GetKeyDown(keys.optionKey))
        { 
            // Change only if PLayer State is Player //
            if(playerState == PlayerState.player)
                SwitchStatePlayer(PlayerState.ui);
        }

        // Check if Build Button is pressed //
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
    public KeyCode playerForwardKey;
    public KeyCode playerBackwardsKey;
    public KeyCode playerLeftKey;
    public KeyCode playerRightKey;
    public KeyCode playerRunKey;
    public KeyCode playerJumpKey;

    [Header("Base Keys")]
    public KeyCode baseForwardKey;
    public KeyCode baseBackwardsKey;
    public KeyCode baseLeftKey;
    public KeyCode baseRightKey;
    public KeyCode baseSwitchCamKey;

    [Header("UI Keys")]
    public KeyCode journalKey;
    public KeyCode inventoryKey;
    public KeyCode mapKey;
    public KeyCode optionKey;

    [Header("Interaction Key")]
    public KeyCode interactionKey;

    [Header("Build Mode Key")]
    public KeyCode buildKey;
}
