using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInteractHitBox interactHitBox = default;

    private PlayerInput input;

    //Check if the player is crushed
    private bool isCollindingRoof;
    private bool isCollindingPlateform;

    private List<RewindableObject> rewindableObjectList = new List<RewindableObject>();

    private void Start()
    {
        input = GetComponent<NewPlayerController>().input;
        interactHitBox = GetComponentInChildren<PlayerInteractHitBox>();

        input.Default.Interact.performed += ctx => OnInteract();
        input.Default.MarkObject.performed += ctx => OnMarkObject();
        input.Default.Rewind.performed += ctx => OnRewindAction();
    }

    private void OnMarkObject()
    {
        if (!interactHitBox.canInteract) return;
        InteractableObject obj = interactHitBox.interactableObject;


        Debug.Log("Interact with an interactable object");
        interactHitBox.interactableObject.OnInteract();
    }

    private void OnRewindAction()
    {
        throw new NotImplementedException();
    }

    private void Update()
    {

        //If the player is colliding with the roof and the plateform
        //The player die
        if (isCollindingRoof && isCollindingPlateform)
        {
            //Kill the player
            Destroy(gameObject);
        }
    }

    private void OnInteract()
    {
        if (!interactHitBox.canInteract) return;

        Debug.Log("Interact with an interactable object");
        interactHitBox.interactableObject.OnInteract();

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Roof"))
        {
            isCollindingRoof = true;
        }

        if (collision.collider.CompareTag("Interactable"))
        {
            isCollindingPlateform = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Roof"))
        {
            isCollindingRoof = false;
        }

        if (collision.collider.CompareTag("Interactable"))
        {
            isCollindingPlateform = false;
        }
    }
}
