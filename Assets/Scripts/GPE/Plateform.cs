using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateform : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player")|| (other.gameObject.tag == "Interactable"))
        {
            other.gameObject.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "Player") || (other.gameObject.tag == "Interactable"))
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
