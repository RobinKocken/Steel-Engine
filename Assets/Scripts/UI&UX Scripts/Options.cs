using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public Keys keys;

    public static float playerMouseSens;
    public float mouseSens;

    public Texture2D tex;

    void Start()
    {
        InitializeKeys();
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {

    }

    // Assigning Keys //
    void InitializeKeys()
    {
        // Player Keys assigned //
        Keys.forward = keys.forwardKey;
        Keys.backwards = keys.backwardsKey;
        Keys.left = keys.leftKey;
        Keys.right = keys.rightKey;
        Keys.jump = keys.jumpKey;

        // Inventory Key assigned //
        Keys.inventory = keys.inventorykey;

        // Interaction Key assigned //
        Keys.interaction = keys.interactionKey;

        // Build Key assigned //
        Keys.build = keys.buildKey;
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
    [Header("Player Keys")]
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

    [Header("Inventory Key")]
    public KeyCode inventorykey;
    public static KeyCode inventory;

    [Header("Interaction Key")]
    public KeyCode interactionKey;
    public static KeyCode interaction;

    [Header("Build")]
    public KeyCode buildKey;
    public static KeyCode build;
}
