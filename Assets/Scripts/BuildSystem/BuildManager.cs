using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{

    public GameManager gameManager;
    public Transform buildingParent;

    [SerializeField] private LayerMask layerMask;
    [Header("Objects")]
    public GameObject[] objects;
    public GameObject pendingObj;
    public float rotateAmount;
    public bool canPlace;

    [Header("Grid Stuff")]
    public float offset;
    public float gridSize;
    public int gridHeight;
    public int yPos;
    private bool gridOn = true;
    private Vector3 pos;

    private RaycastHit hit;

    public void SelectObject(int index)
    {
        pendingObj = Instantiate(objects[index], pos, transform.rotation, buildingParent);
        pendingObj.GetComponent<CheckPlacement>().buildingID = index;
        Debug.Log("building object");
    }

    void RotateObject()
    {
        pendingObj.transform.Rotate(Vector3.up, rotateAmount);
    }

    public void BuildInput()
    {
        if(pendingObj != null)
        {
            //check if grid is on, and snap objects if its on
            if(gridOn)
            {
                pendingObj.transform.localPosition = new Vector3(
                RoundToNearestGrid(pos.x),
                yPos + offset,
                RoundToNearestGrid(pos.z));
            }
            else
            {
                pendingObj.transform.localPosition = new Vector3(pos.x, yPos + offset, pos.z);
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                yPos += gridHeight;
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                yPos -= gridHeight;
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
        if(Physics.Raycast(gameManager.playerCamera.transform.position, gameManager.playerCamera.GetComponent<RaycastController>().transform.forward, out hit, 1000, layerMask))
        {
            pos = new Vector3(hit.point.x, hit.point.y + offset , hit.point.z);
            pos -= transform.position;
        }
    }

    //snap cords of object to nearest int
    float RoundToNearestGrid(float pos)
    {
        //Changeable grid system
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if (xDiff > (gridSize / 2))
        {
            pos += gridSize;
        }
        //pos = Mathf.RoundToInt(pos);
        return pos;
    }
}
