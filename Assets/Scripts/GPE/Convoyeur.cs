using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convoyeur : RewindableObject  
{
    public float speed;
    public bool isOn;
    public bool isRewind = false;
    
    void Start()
    {
        
    }
    
    private void OnTriggerStay(Collider other)
    {       
            if (isOn)
            {
                if (isRewind )
                {
                    Debug.Log("touch");

                // Move the object on top of the Conveyor Belt
                if (other.gameObject.CompareTag("Player")|| other.gameObject.CompareTag("Player")) other.gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
                }
                else
                {
                    Debug.Log("touch");

                // Move the object on top of the Conveyor Belt
                if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player")) other.gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
               
            }    

            
    }
    override protected void OnRewind()
    {
        if (isOn)
        {
            isRewind = true;
        }
    }

}
