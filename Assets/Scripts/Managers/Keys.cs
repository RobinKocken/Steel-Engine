using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
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
    public KeyCode previousSelect;
    public KeyCode nextSelect;

    [Header("Interaction Key")]
    public KeyCode interactionKey;

    [Header("Build Mode Key")]
    public KeyCode buildKey;
}
