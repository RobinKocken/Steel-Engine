using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    GameManager gameManager;
    public GameObject selectedObj;
    public TextMeshProUGUI objText;
    public GameObject selectUI;

    private void Start()
    {
        gameManager =  GetComponent<BuildManager>().gameManager;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = gameManager.buildCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000))
            {
                if(hit.collider.gameObject.CompareTag("Building"))
                {
                    Select(hit.collider.gameObject);
                }
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            Deselect();
        }
    }
    //Selecting the Object for further input
    void Select(GameObject target)
    {
        Debug.Log("Selecting Object");
        if (target == selectedObj)
        {
            return;
        }
        if (selectedObj == null)
        {
            return;
        }
        if (selectedObj != null)
        {
            Deselect();
        }
        Outline outline = target.GetComponent<Outline>();
        if(outline == null)
        {
            target.AddComponent<Outline>();
        }
        else
        {
            outline.enabled = true;
            objText.text = target.name;
            selectedObj = target;
            selectUI.SetActive(true);
        }
    }

    public void Move()
    {
        gameObject.GetComponent<BuildManager>().pendingObj = selectedObj;
    }
    //Deselecting the selected object
    void Deselect()
    {
        if(selectedObj != null)
        {
            selectedObj.GetComponent<Outline>().enabled = false;
            selectedObj = null;
            selectUI.SetActive(false);
        }
        
    }
    public void Delete()
    {
        GameObject objToDestroy = selectedObj;
        Deselect();
        Destroy(objToDestroy);
    }
}
