using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : InteractableObject
{
    [SerializeField] private bool leverOnOff;
    [SerializeField] private bool conveyorDirectionOnly;
    [Header("Only put the objects containing their script, not the parent")]
    [SerializeField] GameObject[] linkedObjects;
    public void Start()
    {
        leverOnOff = false;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
        }
    }
    public override void OnInteract()
    {
        base.OnInteract();
        leverOnOff = true;
        Debug.Log("on");
        if (conveyorDirectionOnly)
        {
            foreach (GameObject _go in linkedObjects)
            {
                if (_go.name.Contains("Conveyor")) // Conveyor Belt
                {
                    _go.GetComponent<Conveyor>().speed = -_go.GetComponent<Conveyor>().speed;
                }
               
            }
        }
        else
        {
            foreach (GameObject _go in linkedObjects)
            {

                if (_go.name.Contains("Moving")) // Moving Platform
                {
                    _go.GetComponent<MouvingPlatform1>().isOn = !_go.GetComponent<MouvingPlatform1>().isOn;

                    Debug.Log("Changed moving platform");
                }
                else if (_go.name.Contains("Conveyor")) // Conveyor Belt
                {
                    _go.GetComponent<Conveyor>().isOn = !_go.GetComponent<Conveyor>().isOn;
                }
                else if (_go.name.Contains("TrapDoor")) // Trap Door
                {
                    //Debug.Log("yo");
                    _go.GetComponent<TrapDoor>().OnInteract();
                }
            }
        }



    }
}

