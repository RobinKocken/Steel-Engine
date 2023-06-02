using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    [SerializeField] private BuildManager buildManager;
    public int buildingID;
    // Start is called before the first frame update
    void Start()
    {
        buildManager = GameObject.Find("Buildings").GetComponent<BuildManager>();
        buildManager.canPlace = true;
    }
    //If the Object colides with another object, make it unable to be placed
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.TryGetComponent(out CheckPlacement check)) 
        {
            if(check != null)
            {
                buildManager.canPlace = false;
            }
        }    
    }
    //If the Object does not colide with another object, make it able to be placed
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CheckPlacement check))
        {
            if (check != null)
            {
                buildManager.canPlace = true;
            }
        }
    }
}
