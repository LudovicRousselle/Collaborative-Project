using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : RewindableObject
{
    [SerializeField] private float fallingSpeed;
    [SerializeField] private bool isFalling;
    [SerializeField] private Collider triggerCollider;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = isFalling = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");

        if (other.gameObject.tag == "Player" && !isFalling)
        {
            isFalling = true;
            Destroy(triggerCollider.gameObject);
        }
    }

    //override protected void OnRewind()
    //{
    //    Debug.Log("Rewind");
    //    fallingSpeed = -Mathf.Abs(fallingSpeed);
    //    SetStateVoid();
    //}

    protected override void DoAction()
    {
        fallingSpeed = Mathf.Abs(fallingSpeed);
        SetStateVoid();
    }

    protected override void OnVoid()
    {
        if (isFalling)
        {
            rb.velocity = Vector3.up * -fallingSpeed;
        }
    }

}
