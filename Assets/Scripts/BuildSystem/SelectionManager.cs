using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject selectedObj;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 1000))
            {
                if(hit.collider.gameObject.CompareTag("Building"))
                {
                    Select(hit.collider.gameObject);
                }
            }
        }
    }

    void Select(GameObject target)
    {
        if(target == selectedObj)
        {
            return;
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
}
