using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    public GameManager gameManager;
    public InventoryManager inventoryManager;

    public float rayDistance;
    public LayerMask layerMask;

    IInteractable iInteractable;
    bool canInteract;

    public bool GetCanInteract
    {
        get
        {
            return canInteract;
        }
    }

    public IInteractable Interactable
    {
        get
        {
            return iInteractable;
        }
    }

    public void GetInteractionKeyInput(KeyCode kInteraction)
    {
        PlayerRaycast(kInteraction);
    }

    void PlayerRaycast(KeyCode kInteraction)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.red);

        if(Physics.Raycast(ray, out RaycastHit hit, rayDistance, layerMask))
        {
            if(hit.transform.TryGetComponent<IInteractable>(out IInteractable iInteractable))
            {
                canInteract = true;
                this.iInteractable = iInteractable;
            }

            else
            {
                canInteract = false;
                this.iInteractable = null;
            }
        }
    }
}
