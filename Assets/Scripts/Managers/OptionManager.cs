using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    public enum OptionState
    {
        none,
        gameplay,
        graphics,
        sound,
        controls,
    }

    public OptionState state;

    public static float playerMouseSens;
    public float mouseSens;
    public Texture2D tex;

    public TopUI topUI;
    public GameplayUI gameplayUI;
    public GraphicsUI graphicsUI;
    public SoundUI soundUI;
    public ControlsUI controlsUI;

    void Start()
    {
        //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        playerMouseSens = mouseSens;
        TopButtonSelect(0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            TopButtonSelect(topUI.currentTargetIndex - 1);

        if(Input.GetKeyDown(KeyCode.Alpha3))
            TopButtonSelect(topUI.currentTargetIndex + 1);
    }

    void State(int stateIndex)
    {
        state = (OptionState)stateIndex + 1;

        switch(state)
        {
            case OptionState.none:
            {
                SettingsChange(false, false, false, false);
                break;
            }
            case OptionState.gameplay:
            {
                SettingsChange(true, false, false, false);
                break;
            }
            case OptionState.graphics:
            {
                SettingsChange(false, true, false, false);
                break;
            }
            case OptionState.sound:
            {
                SettingsChange(false, false, true, false);
                break;
            }
            case OptionState.controls:
            {
                SettingsChange(false, false, false, true);
                break;
            }
        }
    }

    public void SettingsChange(bool gameplayActive, bool graphicsActive, bool soundActive, bool controlsActive)
    {
        gameplayUI.gameplayUIParent.SetActive(gameplayActive);
        graphicsUI.graphicsUIParent.SetActive(graphicsActive);
        soundUI.soundUIParent.SetActive(soundActive);
        controlsUI.controlsUIParent.SetActive(controlsActive);
    }

    public void TopButtonSelect(int targetIndex)
    {
        if(targetIndex < 0 || targetIndex > topUI.barTarget.Length - 1)
            return;
        else
        {
            State(targetIndex);

            StopCoroutine(BarAnimation(-1));
            StartCoroutine(BarAnimation(targetIndex));
        }
    }

    IEnumerator BarAnimation(int targetIndex)
    {
        topUI.coroutineRun = true;
        topUI.currentTargetIndex = targetIndex;

        while(topUI.coroutineRun)
        {
            topUI.bar.localPosition = Vector2.MoveTowards(topUI.bar.localPosition, topUI.barTarget[targetIndex].localPosition, topUI.barSpeed * Time.deltaTime);
            topUI.bar.sizeDelta = Vector2.MoveTowards(topUI.bar.sizeDelta, topUI.barTarget[targetIndex].sizeDelta, topUI.barSpeed * Time.deltaTime);

            if(topUI.bar.localPosition == topUI.barTarget[targetIndex].localPosition && topUI.bar.sizeDelta == topUI.barTarget[targetIndex].sizeDelta)
            {
                topUI.coroutineRun = false;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public void LoadSceneButton(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

[System.Serializable]
public class TopUI
{
    public RectTransform bar;
    public float barSpeed;

    public int currentTargetIndex;

    public bool coroutineRun;

    public RectTransform[] barTarget;
}

[System.Serializable]
public class GameplayUI 
{
    public GameObject gameplayUIParent;
}

[System.Serializable]
public class GraphicsUI 
{
    public GameObject graphicsUIParent;
}

[System.Serializable]
public class SoundUI
{
    public GameObject soundUIParent;
}

[System.Serializable]
public class ControlsUI
{
    public GameObject controlsUIParent;
}