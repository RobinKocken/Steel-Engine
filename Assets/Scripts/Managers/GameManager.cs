using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;

    public enum PlayerState
    {
        player,
        station,
    }
    public PlayerState state;

    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        switch(state)
        {
            case PlayerState.player:
            {
                break;
            }
            case PlayerState.station:
            {
                break;
            }
        }
    }

    // Getting the Input
    void GetInput()
    {
        // Forward Key
        if(Input.GetKeyDown(Keys.forward))
        {

        }
        else if(Input.GetKeyUp(Keys.forward))
        {

        }

        // Backwards Key
        if(Input.GetKeyDown(Keys.backwards))
        {

        }
        else if(Input.GetKeyUp(Keys.backwards))
        {

        }

        // Left Key
        if(Input.GetKeyDown(Keys.left))
        {

        }
        else if(Input.GetKeyUp(Keys.left))
        {

        }

        // Right Key
        if(Input.GetKeyDown(Keys.right))
        {

        }
        else if(Input.GetKeyUp(Keys.right))
        {

        }
    }
}
