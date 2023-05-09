using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public Keys keys;

    public static float playerMouseSens;
    public float mouseSensDebug;

    void Start()
    {
        InitializeKeys();
    }

    void Update()
    {
        DebugTemporary();
    }

    void DebugTemporary()
    {
        playerMouseSens = mouseSensDebug;
    }

    void InitializeKeys()
    {
        Keys.forward =keys.forwardKey;
        Keys.backwards = keys.backwardsKey;
        Keys.left = keys.leftKey;
        Keys.right = keys.rightKey;
        Keys.jump = keys.jumpKey;
    }

    //void OnGUI()
    //{
    //    Event e = Event.current;
    //    if(e.isKey)
    //    {
    //        Debug.Log("Detected key code: " + e.keyCode);
    //        b.forwardNromal = e.keyCode;
    //    }
    //}
}

[System.Serializable]
public class Keys
{
    public KeyCode forwardKey;
    public static KeyCode forward;

    public KeyCode backwardsKey;
    public static KeyCode backwards;

    public KeyCode leftKey;
    public static KeyCode left;

    public KeyCode rightKey;
    public static KeyCode right;

    public KeyCode jumpKey;
    public static KeyCode jump;
}
