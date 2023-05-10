using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    private BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        buildManager = GameObject.Find("BuildManager").GetComponent<BuildManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Building"))   
        {
            buildManager.canPlace = false;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Building"))
        {
            buildManager.canPlace = true;
        }
    }
}
