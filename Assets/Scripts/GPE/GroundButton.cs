using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundButton : MonoBehaviour
{
    public bool isPress= false;
    public float buttonDown = 0.12f;
    public float buttonUp = 0.06f;

    private void OnCollisionStay(Collision collision)
    {
        if ((collision.gameObject.tag == "SBlock"))
        {
            isPress = true;
            Debug.Log("touch");
            transform.position += new Vector3(0, - buttonDown, 0 ) * Time.deltaTime;

        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "SBlock"))
        {
            isPress = false;
            Debug.Log("touch");
            transform.position -= new Vector3(0, -buttonDown, 0) * Time.deltaTime;
        }
            
    }

}