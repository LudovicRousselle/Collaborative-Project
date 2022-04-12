using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInteractHitBox interactHitBox = default;

    private PlayerInput input;

    private void Start()
    {
        input = GetComponent<PlayerController>().input;
        interactHitBox = GetComponentInChildren<PlayerInteractHitBox>();

        input.Default.Interact.performed += ctx => OnInteract();
    }

    private void OnInteract()
    {
        if (!interactHitBox.canInteract) return;

        Debug.Log("Interact with an interactable object");
        interactHitBox.interactableObject.OnInteract();
    }
}
