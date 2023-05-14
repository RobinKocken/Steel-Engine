using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;
    public RaycastController raycastController;

    public Options options;
    public UIManager uiManager;

    public GameObject buildManager;

    public GameObject playerCamera;
    public GameObject buildCamera;

    // Possible States for the Player //
    public enum PlayerState
    {
        player,
        station,
        build,
        ui,
    }
    public PlayerState state;

    // Input Value Player Movement //
    int iForward, iBackwards, iLeft, iRight, iJump;
    // Input Value Inventory //
    int iInventory;
    // Input Value Interaction //
    int iInteraction;

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    public string testKey;

    void Update()
    {
        CheckKeyInput();
        SwitchState();
        State();

        if(Input.anyKeyDown)
        {
            testKey = Input.inputString;
        }
    }

    // State of the Player //
    void State()
    {
        switch(state)
        {
            case PlayerState.player:
            {
                CursorModeLocked();
                SwitchCamera(playerCamera, buildCamera);
                playerController.GetKeyInput(iForward, iBackwards, iLeft, iRight, iJump);
                raycastController.GetKeyInput(iInteraction);

                //buildManager.SetActive(false);
                break;
            }
            case PlayerState.station:
            {
                break;
            }
            case PlayerState.build:
            {
                CursorModeConfined();
                SwitchCamera(buildCamera, playerCamera);
                buildManager.SetActive(true);
                break;
            }
            case PlayerState.ui:
            {
                CursorModeConfined();
                break;
            }
        }
    }

    void SwitchState()
    {
        uiManager.ResetUIInput(iInventory);

        if(iInventory == 1 && uiManager.uiIsReady) 
        {
            switch(state)
            {
                case PlayerState.player:
                {
                    ResetPlayerMovement();
                    state = PlayerState.ui;
                    break;
                }
                case PlayerState.ui:
                {
                    state = PlayerState.player;
                    break;
                }
            }

            uiManager.Inventory();
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

    void ResetPlayerMovement()
    {
        playerController.GetKeyInput(0, 0, 0, 0, 0);
    }

    // Getting Input from Keyboard //
    (int tForward, int tBackwards, int tLeft, int tRight, int tJump, int tInventory, int tInteraction) CheckKeyInput()
    {
        // Forward Key PLayer //
        if(Input.GetKeyDown(Keys.forward))
            iForward = 1;             
        else if(Input.GetKeyUp(Keys.forward))
            iForward = 0;         

        // Backwards Key Player //
        if(Input.GetKeyDown(Keys.backwards))
            iBackwards = 1;
        else if(Input.GetKeyUp(Keys.backwards))
            iBackwards = 0;

        // Left Key Player //
        if(Input.GetKeyDown(Keys.left))
            iLeft = 1;
        else if(Input.GetKeyUp(Keys.left))
            iLeft = 0;

        // Right Key Player //
        if(Input.GetKeyDown(Keys.right))
            iRight = 1;
        else if(Input.GetKeyUp(Keys.right))
            iRight = 0;

        // Jump Key Player
        if(Input.GetKeyDown(Keys.jump))
            iJump = 1;
        else if(Input.GetKeyUp(Keys.jump))
            iJump = 0;

        // Inventory Key Player //
        if(Input.GetKeyDown(Keys.inventory))
            iInventory = 1;
        else if(Input.GetKeyUp(Keys.inventory))
            iInventory = 0;

        // Interaction Key Player //
        if(Input.GetKeyDown(Keys.interaction))
            iInteraction = 1;
        else if(Input.GetKeyUp(Keys.interaction))
            iInteraction = 0;

        return (iForward, iBackwards, iLeft, iRight, iJump, iInventory, iInteraction);
    }
}
