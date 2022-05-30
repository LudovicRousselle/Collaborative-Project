using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundButton : MonoBehaviour
{
    public bool isPressed;
    public float buttonDown = 0.12f;
    public float buttonUp = 0.06f;

    [Header("Only put the objects containing their script, not the parent")]
    [SerializeField] GameObject[] linkedObjects; 


   
    private void OnCollisionStay(Collision collision)
    {
        if (!((collision.gameObject.tag == "Rewindable") || (collision.gameObject.tag == "SBlock"))) return;
            // Lancé à une seule frame
        if (!isPressed)
        {
            isPressed = true;
            Debug.Log("Button gets activated");
            ActivateObjects();
        }            
            
        //transform.position += new Vector3(0, -buttonDown, 0) * Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!((other.gameObject.tag == "Rewindable") || (other.gameObject.tag == "SBlock"))) return;

        isPressed = false;
        Debug.Log("Button gets deactivated");
        ActivateObjects();
        //transform.position -= new Vector3(0, -buttonDown, 0) * Time.deltaTime;
    }

    // Méthode shlag pour on/off des objets quand le bouton est activé
    void ActivateObjects()
    {
        foreach (GameObject _go in linkedObjects)
        {
            if (_go.name.Contains("Moving")) // Moving Platform
            {
                _go.GetComponent<MouvingPlatform1>().isOn = !_go.GetComponent<MouvingPlatform1>().isOn;

                Debug.Log("Changed moving platform");
            }
            else if ( _go.name.Contains("Conveyor")) // Conveyor Belt
            {
                _go.GetComponent<Conveyor>().isOn = !_go.GetComponent<Conveyor>().isOn;
            }
            else if (_go.GetComponent<Door>() != null) _go.GetComponent<Door>().OnInteract();
            else if (_go.GetComponent<TrapDoor>() != null) _go.GetComponent<TrapDoor>().OnInteract();
            
        }
    }
}
