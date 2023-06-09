using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    public static float playerMouseSens;
    public float mouseSens;

    public Texture2D tex;

    void Start()
    {
        //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        
    }

    void LoadSceneButton(int i)
    {
        SceneManager.LoadScene(i);
    }
}
