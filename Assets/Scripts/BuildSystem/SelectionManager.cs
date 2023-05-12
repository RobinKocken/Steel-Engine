using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    GameManager gameManager;
    public GameObject selectedObj;

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

    void Select(GameObject target)
    {
        if(target == selectedObj)
        {
            return;
        }
        if(selectedObj != null)
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
            selectedObj = target;
        }
    }

    void Deselect()
    {
        selectedObj.GetComponent<Outline>().enabled = false;
        selectedObj = null;
    }
}
