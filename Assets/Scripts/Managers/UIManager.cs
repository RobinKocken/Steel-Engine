using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public InventoryManager inventoryManager;

    public enum InternalUIState
    {
        none,
        journal,
        inventory,
        map,
        option,
    }
    public InternalUIState internalUIState;

    public enum ExternalUIState
    {
        none,
        build,
        craft,
        farm,
    }
    public ExternalUIState externalUIState;

    public GameObject uiPlayer;
    public InternalUI internalUI;
    public ExternalUI externalUI;
    public AnimationUI animationUI;

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

    public void InternalUIUpdate(KeyCode journalKey ,KeyCode inventoryKey, KeyCode mapKey)
    {
        if(Input.GetKeyDown(journalKey))
        {
            if(internalUIState != InternalUIState.journal)
            {
                StateUI(InternalUIState.journal, ExternalUIState.none);
            }
            else
            {
                StateUI(InternalUIState.none, ExternalUIState.none);
                gameManager.SwitchStatePlayer(GameManager.PlayerState.player);
            }
        }

        if(Input.GetKeyDown(inventoryKey))
        {
            if(internalUIState != InternalUIState.inventory)
            {
                StateUI(InternalUIState.inventory, ExternalUIState.none);
            }
            else
            {
                StateUI(InternalUIState.none, ExternalUIState.none);
                gameManager.SwitchStatePlayer(GameManager.PlayerState.player);
            }
        }

        if(Input.GetKeyDown(mapKey))
        {
            if(internalUIState != InternalUIState.map)
            {
                StateUI(InternalUIState.map, ExternalUIState.none);
            }
            else
            {
                StateUI(InternalUIState.none, ExternalUIState.none);
                gameManager.SwitchStatePlayer(GameManager.PlayerState.player);
            }
        }

        inventoryManager.InventoryUpdate();
    }

    public void ExternalUIUpdate(KeyCode interactionKey)
    {
        if (Input.GetKeyDown(interactionKey))
        {
            StateUI(InternalUIState.none, ExternalUIState.none);
            gameManager.SwitchStatePlayer(GameManager.PlayerState.player);
        }
    }

    public void StateUI(InternalUIState iInternalUIState, ExternalUIState eExternalUIState)
    {
        internalUIState = iInternalUIState;
        externalUIState = eExternalUIState;

        if(internalUIState != InternalUIState.none)
        {
            Player(false);
            switch(internalUIState)
            {
                case InternalUIState.journal:
                {
                    StartCoroutine(BarAnimation(animationUI.questBar));
                    Internal(true, false, false, false);
                    break;
                }
                case InternalUIState.inventory:
                {
                    StartCoroutine(BarAnimation(animationUI.inventoryBar));
                    Internal(false, true, false, false);
                    break;
                }
                case InternalUIState.map:
                {
                    StartCoroutine(BarAnimation(animationUI.mapBar));
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
            Player(false);
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
            Player(true);
        }
    }

    void Internal(bool journalActive, bool inventoryActive, bool mapActive, bool optionActive)
    {
        if(journalActive || inventoryActive || mapActive || optionActive)
            internalUI.uiInternal.SetActive(true);
        else
            internalUI.uiInternal.SetActive(false);

        internalUI.uiJournal.SetActive(journalActive);
        internalUI.uiInventory.SetActive(inventoryActive);
        internalUI.uiMap.SetActive(mapActive);
        internalUI.uiOption.SetActive(optionActive);
    }

    void External(bool builActive, bool craftActive, bool farmActive)
    {
        if(builActive || craftActive || farmActive)
            externalUI.uiExternal.SetActive(true);
        else
            externalUI.uiExternal.SetActive(false);

        externalUI.uiBuild.SetActive(builActive);
        externalUI.uiCraft.SetActive(craftActive);
        externalUI.uiFarm.SetActive(farmActive);
    }

    void Player(bool active)
    {
        uiPlayer.SetActive(active);
    }

    public void TopButtons(int stateValue)
    {
        StateUI((InternalUIState)stateValue, ExternalUIState.none);
    }

    IEnumerator BarAnimation(RectTransform targetBar)
    {
        bool run = true;
        while(run)
        {
            animationUI.bar.localPosition = Vector2.MoveTowards(animationUI.bar.localPosition, targetBar.localPosition, animationUI.barSpeed * Time.deltaTime);
            animationUI.bar.sizeDelta = Vector2.MoveTowards(animationUI.bar.sizeDelta, targetBar.sizeDelta, animationUI.barSpeed * Time.deltaTime);

            if(animationUI.bar.localPosition == targetBar.localPosition && animationUI.bar.sizeDelta == targetBar.sizeDelta)
            {
                run = false;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}

[System.Serializable]
public class InternalUI
{
    public GameObject uiInternal;
    public GameObject uiJournal;
    public GameObject uiInventory;
    public GameObject uiMap;
    public GameObject uiOption;
}

[System.Serializable]
public class ExternalUI 
{
    public GameObject uiExternal;
    public GameObject uiBuild;
    public GameObject uiCraft;
    public GameObject uiFarm;
}

[System.Serializable]
public class AnimationUI 
{
    public RectTransform bar;
    public float barSpeed;

    public RectTransform questBar;
    public RectTransform inventoryBar;
    public RectTransform mapBar;
}


