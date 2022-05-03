using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private void Awake()
    {
        if (gameObject.tag != "Interactable")
        {
            gameObject.tag = "Interactable";
        }
    }

    public virtual void OnInteract()
    {
        Debug.Log("Inside Interact");
    }
}
