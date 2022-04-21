using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : RewindableObject
{
    [SerializeField] float fallingSpeed;
    [SerializeField] bool isFalling;
    [SerializeField] Collider triggerCollider;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = isFalling = false;
    }


    private void Update()
    {
        if (isFalling)
        {
            rb.velocity = Vector3.up * -fallingSpeed;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnRewind();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnProceed();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && triggerCollider != null)
        {
            isFalling = true;
            Destroy(triggerCollider.gameObject);
        }else if (other.gameObject.tag == "Player" || other.gameObject.tag == "Interactable")
        {
            //Kill the player if under the box
            Destroy(other.gameObject);
        }
    }

    override protected void OnRewind()
    {
        Debug.Log("Rewind");
        fallingSpeed = -Mathf.Abs(fallingSpeed);
        SetStateVoid();
    }

    protected override void OnProceed()
    {
        fallingSpeed = Mathf.Abs(fallingSpeed);
        SetStateVoid();
    }

}
