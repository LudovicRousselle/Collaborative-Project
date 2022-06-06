using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractHitBox : MonoBehaviour
{
    [SerializeField] private string tagInteractable = "Interactable";
    [SerializeField] private string tagRewindable = "Rewindable";

    [HideInInspector] public bool canInteract = false;
    [HideInInspector] public bool canMark = false;
    [HideInInspector] public InteractableObject interactableObject;
    public List<RewindableObject> rewindableObject = new List<RewindableObject>();

    private void Start()
    {
        rewindableObject.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagInteractable))
        {
            canInteract = true;
            interactableObject = other.gameObject.GetComponent<InteractableObject>();
        }
        else if (other.gameObject.CompareTag(tagRewindable))
        {
            rewindableObject.Add(other.gameObject.GetComponent<RewindableObject>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tagInteractable))
        {
            canInteract = false;
            interactableObject = null;
        }
        else if (other.gameObject.CompareTag(tagRewindable))
        {
            if(rewindableObject.Contains(other.gameObject.GetComponent<RewindableObject>())) 
                rewindableObject.Remove(other.gameObject.GetComponent<RewindableObject>());
        }
    }
}
