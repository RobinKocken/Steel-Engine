using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    public float rayDistance;
    public LayerMask layerMask;

    // Input value for Interaction Key //
    int kInteraction;
    bool readyToInteract;

    void Start()
    {
        
    }

    void Update()
    {
        CheckIfInteract();
        PlayerRaycast();
    }

    void PlayerRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);

        if(Physics.Raycast(ray, out RaycastHit hit, rayDistance, layerMask))
        {
            Debug.Log("Ray");
            if(hit.transform.TryGetComponent<IInteractable>(out IInteractable iInteractable))
            {
                Debug.Log("Comp");

                if(kInteraction == 1 && readyToInteract)
                {
                    readyToInteract = false;

                    Debug.Log("Interact");
                    iInteractable.Interact();
                }
            }
        }
    }

    void CheckIfInteract()
    {
        if(kInteraction == 0 && !readyToInteract)
        {
            readyToInteract = true;
        }
    }

    public void GetKeyInput(int tInteraction)
    {
        kInteraction = tInteraction;
    }
}
