using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInteractHitBox interactHitBox = default;

    [SerializeField] private float rewindDistance = 5;

    private PlayerInput input;

    //Check if the player is crushed
    private bool isCollindingRoof;
    private bool isCollindingPlateform;

    private List<RewindableObject> rewindableObjectList = new List<RewindableObject>();
    private RewindableObject[] prevRewindedObjectList = new RewindableObject[0];
    private RewindableObject lastMarkedObject = default;

    private void Start()
    {
        input = GetComponent<NewPlayerController>().input;
        interactHitBox = GetComponentInChildren<PlayerInteractHitBox>();

        input.Default.Interact.performed += ctx => OnInteract();
        input.Default.MarkObject.performed += ctx => OnMarkObject();
        input.Default.Rewind.performed += ctx => OnRewindAction();
    }

    private void Update()
    {
        CheckForRewindDist();
        //If the player is colliding with the roof and the plateform
        //The player die
        if (isCollindingRoof && isCollindingPlateform)
        {
            //Kill the player
            Destroy(gameObject);
        }
    }

    private void OnMarkObject()
    {
        if (!interactHitBox.canMark) return;
        RewindableObject obj = interactHitBox.rewindableObject;
        
        if (!rewindableObjectList.Contains(obj))
        {
            Debug.Log("Player => " + obj.name + " is marked");
            rewindableObjectList.Add(obj);
        }

        foreach (var element in prevRewindedObjectList)
        {
            if (element.IsRewinding) element.InterruptRewind();
        }

        lastMarkedObject = obj;
    }

    private void OnRewindAction()
    {
        if (rewindableObjectList.Count == 0)
        {
            print("Player => You didn't mark any object");
            return;
        }

        prevRewindedObjectList = new RewindableObject[rewindableObjectList.Count];
        rewindableObjectList.CopyTo(prevRewindedObjectList, 0);

        foreach (var obj in rewindableObjectList)
        {
            obj.OnInteract();
        }

        rewindableObjectList.Clear();
    }

    private void CheckForRewindDist()
    {
        if (lastMarkedObject == null) return;

        if (!InRangeRewind()) CancelRewind();
    }

    private void CancelRewind()
    {
        if (rewindableObjectList.Count <= 0) return;

        rewindableObjectList.Clear();
        lastMarkedObject = null;
        print("Player : out of range of rewind, all marking of objects canceled");
    }

    private bool InRangeRewind()
    {
        return Vector3.Distance(transform.position, lastMarkedObject.gameObject.transform.position) < rewindDistance; 
    }

    private void OnInteract()
    {
        //if (!interactHitBox.canInteract) return;

        //Debug.Log("Interact with an interactable object");
        //interactHitBox.interactableObject.OnInteract();

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
