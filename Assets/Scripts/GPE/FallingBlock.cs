using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : RewindableObject
{
    [SerializeField] private float fallingSpeed = 100;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rb.useGravity = true;
            rb.AddForce(Physics.gravity * fallingSpeed);

        }
    }
    override protected void OnRewind()
    {
        rb.useGravity = true;
        rb.AddForce(-Physics.gravity * fallingSpeed);
        SetStateVoid();
    }

}
