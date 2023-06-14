using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    public enum OptionState
    {
        none,
        gameplay,
        graphics,
        sounds,
        controls,
    }

    public OptionState state;

    public static float playerMouseSens;
    public float mouseSens;
    public Texture2D tex;

    public TopUI topUI;

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

    public void TopButtonSelect(int targetIndex)
    {
        if(targetIndex < 0 || targetIndex > topUI.barTarget.Length - 1)
            return;
        else
        {
            state = (OptionState)targetIndex + 1;

            StopAllCoroutines();
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
