using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelBase : MonoBehaviour, IInteractable
{
    public void Interact(GameManager gameManager)
    {
        gameManager.SwitchStatePlayer(GameManager.PlayerState.station);
    }

}
