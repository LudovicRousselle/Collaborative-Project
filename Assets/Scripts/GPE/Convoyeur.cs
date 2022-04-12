using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Convoyeur : MonoBehaviour  
{
    public float speed;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.Translate(gameObject.transform.forward * speed);     
    }

    

}
