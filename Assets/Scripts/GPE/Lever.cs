using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : InteractableObject
{
    [SerializeField] private bool leverOnOff;
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
        
    }
}

