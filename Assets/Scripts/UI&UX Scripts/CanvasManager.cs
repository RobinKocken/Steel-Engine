using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public enum CanvasState
    {
        mainMenu,
        game,
    }
    public CanvasState canvasState;

    public GameObject mainMenuUI;
    public GameObject gameUI;

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if(0 == scene.buildIndex)
            canvasState = CanvasState.mainMenu;
        else if(1 == scene.buildIndex)
            canvasState = CanvasState.game;

        switch(canvasState)
        {
            case CanvasState.mainMenu:
            {
                mainMenuUI.SetActive(true);
                gameUI.SetActive(false);

                break;
            }
            case CanvasState.game:
            {
                mainMenuUI.SetActive(false);
                gameUI.SetActive(true);

                break;
            }
        }
    }
}
