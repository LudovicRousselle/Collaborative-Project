using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractHitBox : MonoBehaviour
{
    [SerializeField] private string tagInteractable = "Interactable";
    [SerializeField] private string tagRewindable = "Rewindable";
    public bool canInteract = false;
    public InteractableObject interactableObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagInteractable))
        {
            canInteract = true;
            interactableObject = other.gameObject.GetComponent<InteractableObject>();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        canInteract = false;
        interactableObject = null;
    }
}
