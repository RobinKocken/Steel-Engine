using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{

    public GameManager gameManager;

    [SerializeField] private LayerMask layerMask;
    [Header("Objects")]
    public GameObject[] objects;
    public GameObject pendingObj;
    public float rotateAmount;
    public bool canPlace;

    [Header("Grid Stuff")]
    public float offset;
    public float gridSize;
    public int yPos;
    bool gridOn = true;
    [SerializeField] private Toggle gridToggle;
    private Vector3 pos;

    private RaycastHit hit;
    

    public void SelectObject(int index)
    {
        pendingObj = Instantiate(objects[index], pos, transform.rotation);
        gameObject.GetComponent<SelectionManager>().selectedObj = pendingObj;
    }

    void RotateObject()
    {
        pendingObj.transform.Rotate(Vector3.up, rotateAmount);
    }
    public void Update()
    {
        if(pendingObj != null)
        {
            //check if grid is on, and snap objects if its on
            if(gridOn)
            {
                pendingObj.transform.position = new Vector3(
                RoundToNearestGrid(pos.x),
                yPos + offset,
                RoundToNearestGrid(pos.z));
            }
            else 
            { 
                pendingObj.transform.position = new Vector3(pos.x, yPos + offset, pos.z);
            }
            
            if(Input.GetKeyDown(KeyCode.Q))
            {
                yPos++;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                yPos--;
            }    
            
            if(Input.GetMouseButton(0) && canPlace)
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
        //update objects position to screenpoint posistion
        Ray ray = gameManager.buildCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = new Vector3(hit.point.x, hit.point.y + offset , hit.point.z);
        }
    }

    public void ToggleGrid()
    {
        if(gridToggle.isOn) {
            gridOn = true;
        }
        else {  gridOn = false; }
    }

    //snap cords of object to nearest int
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
