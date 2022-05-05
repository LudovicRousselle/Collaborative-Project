using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float distToGround;
    [SerializeField] private float rayLength = 0.2f;
    private RaycastHit hit;

    private void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    public bool IsGrounded() 
    {
        Debug.DrawRay(transform.position + new Vector3(0, -distToGround), -Vector3.up * rayLength, Color.green, 0.1f);
        return Physics.Raycast(transform.position+new Vector3(0,-distToGround), -Vector3.up, rayLength);
    }
}