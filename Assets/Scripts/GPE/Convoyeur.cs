using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convoyeur : MonoBehaviour  
{
    public float speed;
    public bool onOff;
    void Start()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(onOff==true)
        {
            Debug.Log("touch");
            other.gameObject.transform.Translate(Vector3.right * speed);
        }
            
    }

    

}
