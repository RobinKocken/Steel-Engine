using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;

    // Possible States for the Player //
    public enum PlayerState
    {
        player,
        station,
        build,
        option,
    }
    public PlayerState state;

    // Input Value Player Movement //
    int iForward, iBackwards, iLeft, iRight, iJump;

    void Awake()
    {
        
    }

    void Start()
    {
        CursorModeLocked();
    }

    void Update()
    {
        State();
    }

    // State of the Player //
    void State()
    {
        var(tForward, tBackwards, tLeft, tRight, tJump) = CheckKeyInput();

        switch(state)
        {
            case PlayerState.player:
            {
                playerController.GetKeyInput(tForward, tBackwards, tLeft, tRight, tJump);

                break;
            }
            case PlayerState.station:
            {
                break;
            }
            case PlayerState.build:
            {
                break;
            }
            case PlayerState.option:
            {
                break;
            }
        }
    }

    public static void CursorModeLocked()
    { 
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public static void CursorModeConfined()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Getting Input from Keyboard //
    (int tForward, int tBackwards, int tLeft, int tRight, int tJump) CheckKeyInput()
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

        if(Input.GetKeyDown(Keys.jump))
            iJump = 1;
        else if(Input.GetKeyUp(Keys.jump))
            iJump = 0;

        return (iForward, iBackwards, iLeft, iRight, iJump);
    }
}
