using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadzone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (other.gameObject.tag == "Player")
        {
            //Kill the player if under the box
            Destroy(other.gameObject);
        }
    }
}
