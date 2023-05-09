using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [Header("Objects")]
    public GameObject[] objects;
    public GameObject pendingObj;

    [Header("Grid Stuff")]
    public float gridSize;
    public int yPos;
    bool gridOn = true;
    [SerializeField] private Toggle gridToggle;
    private Vector3 pos;

    private RaycastHit hit;
    

    public void SelectObject(int index)
    {
        pendingObj = Instantiate(objects[index], pos, transform.rotation);
    }

    public void Update()
    {
        if(pendingObj != null)
        {
            if(gridOn)
            {
                pendingObj.transform.position = new Vector3(
                RoundToNearestGrid(pos.x),
                RoundToNearestGrid(yPos),
                RoundToNearestGrid(pos.z));
            }
            else 
            { 
                pendingObj.transform.position = new Vector3(pos.x, yPos,pos.z);
            }
            
            if(Input.GetKeyDown(KeyCode.Q))
            {
                yPos++;
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                yPos--;
            }    
            
            if(Input.GetMouseButton(0))
            {
                PlaceObject();
            }
        }
    }

    void PlaceObject()
    {
        pendingObj = null;
        yPos = 0;
    }
    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }

    public void ToggleGrid()
    {
        if(gridToggle.isOn) {
            gridOn = true;
        }
        else {  gridOn = false; }
    }
    float RoundToNearestGrid(float pos)
    {
        //Changeable grid system
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if(xDiff > (gridSize/2))
        {
            pos += gridSize;
        }
        return pos;
    }
}
