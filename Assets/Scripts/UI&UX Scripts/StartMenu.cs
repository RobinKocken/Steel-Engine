using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public OptionManager option;

    public GameObject startUI;
    public GameObject optionsUI;

    void Start()
    {
        StartMenuButton();
    }

    public void SettingsButton()
    {
        optionsUI.SetActive(true);
        startUI.SetActive(false);
        option.state = OptionManager.OptionState.gameplay;
    }

    public void StartMenuButton()
    {
        startUI.SetActive(true);
        optionsUI.SetActive(false);
        option.state = OptionManager.OptionState.none;
    }

    public void LoadSceneButton()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
