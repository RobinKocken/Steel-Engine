using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public RaycastController raycastController;
    public InventoryManager inventoryManager;


    public Options options;
    public UIManager uiManager;

    public BuildManager buildManager;

    public GameObject playerCamera;
    public GameObject buildCamera;

    // Possible States for the Player //
    public enum PlayerState
    {
        player,
        station,
        build,
        internalUI,
        externalUI,
    }
    public PlayerState playerState;

    void Awake()
    {
        
    }

    void Start()
    {
        CursorModeLocked();
    }

    public string testKey;

    void Update()
    {
        GetSwitchStateKeyInput(Keys.inventory, Keys.inventory, Keys.inventory, Keys.inventory, Keys.build, Keys.interaction);
        StatePlayer();

        if(Input.anyKeyDown)
        {
            testKey = Input.inputString;
        }
    }

    // State of the Player //
    void StatePlayer()
    {
        switch(playerState)
        {
            case PlayerState.player:
            {
                playerController.GetPlayerKeyInput(Keys.forward, Keys.backwards, Keys.left, Keys.right, Keys.jump);
                raycastController.GetInteractionKeyInput(Keys.interaction);
                break;
            }
            case PlayerState.station:
            {
                break;
            }
            case PlayerState.build:
            {
                // Delete Later //
                if(buildManager != null)
                {
                    buildManager.BuildInput();
                }
                break;
            }
            case PlayerState.internalUI:
            {
                uiManager.StateUI();
                break;
            }
            case PlayerState.externalUI:
            {
                break;
            }
        }
    }

    void GetSwitchStateKeyInput(KeyCode quests, KeyCode kInventory, KeyCode kMap, KeyCode kOptions, KeyCode kBuild, KeyCode kInteraction)
    {
        // That should be cleaner //
        if(Input.GetKeyDown(quests) || Input.GetKeyDown(kInventory) || Input.GetKeyDown(kMap) || Input.GetKeyDown(kOptions))
        {
            switch(playerState)
            {
                case PlayerState.player:
                {
                    SwitchStatePlayer(PlayerState.internalUI);
                    break;
                }
                case PlayerState.internalUI:
                {
                    SwitchStatePlayer(PlayerState.player);
                    break;
                }
            }
        }

        if(Input.GetKeyDown(kBuild))
        {
            switch(playerState)
            {
                case PlayerState.player:
                {
                    SwitchStatePlayer(PlayerState.build);
                    break;
                }
                case PlayerState.build:
                {
                    SwitchStatePlayer(PlayerState.player);
                    break;
                }
            }
        }

        if(raycastController.GetCanInteract == true)
        {
            if(Input.GetKeyDown(kInteraction))
            {
                //raycastController.Interactable.Interact(GameManager, inventoryManager);

            }

        }
    }

    // Make from this a cleaner Version //
    void SwitchStatePlayer(PlayerState pPlayerState)
    {
        playerState = pPlayerState;

        switch(playerState)
        {
            case PlayerState.player:
            {
                CursorModeLocked();
                uiManager.Inventory(false);
                SwitchCamera(playerCamera, buildCamera);
                break;
            }
            case PlayerState.station:
            {
                CursorModeLocked();
                playerController.StopMovement();
                break;
            }
            case PlayerState.build:
            {
                CursorModeConfined();
                playerController.StopMovement();
                SwitchCamera(buildCamera, playerCamera);
                break;
            }
            case PlayerState.internalUI:
            {
                CursorModeConfined();
                playerController.StopMovement();
                uiManager.Inventory(true);
                break;
            }
            case PlayerState.externalUI:
            {
                CursorModeConfined();
                playerController.StopMovement();

                break;
            }
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

        Options.playerMouseSens = options.mouseSens;
    }

    void CursorModeConfined()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        Options.playerMouseSens = 0;
    }
}
