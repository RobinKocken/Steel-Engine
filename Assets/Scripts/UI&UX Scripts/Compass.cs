using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public RawImage compass;
    public Transform player;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        compass.uvRect = new Rect(player.localEulerAngles.y / 360, 0, 1, 1);    
    }
}
