using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convoyeur : MonoBehaviour  
{
    public float speed;
    public bool isOn;
    
    void Start()
    {
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        if(isOn)
        {
            Debug.Log("touch");
            
            // Move the object on top of the Conveyor Belt
            other.gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
            
    }

    

}
