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
    //If the Object colides with another object, make it unable to be placed
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Building"))   
        {
            buildManager.canPlace = false;
        }    
    }
    //If the Object does not colide with another object, make it able to be placed
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Building"))
        {
            buildManager.canPlace = true;
        }
    }
}
