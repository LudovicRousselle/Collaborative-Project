using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFallingBlock : RewindableObject
{
    [SerializeField] private float fallingSpeed;
    [SerializeField] public string targetTag = "Player";
    [SerializeField] public string groundTag = "Ground";

    private FallingBlockTriggerZone triggerZone;
    private Rigidbody rb;
    private bool triggered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        triggerZone = GetComponentInChildren<FallingBlockTriggerZone>();

        if (triggerZone == null)
        {
            Debug.LogError("The falling block object needs a trigger hitbox (place it as a child with a trigger hitbox with the FallingBoxTriggerZone.cs)");
        }
    }

    protected override void DoAction()
    {
        rb.AddForce(-Vector3.up * fallingSpeed);
    }

    protected override void OnVoid()
    {
        if (triggerZone.triggered || triggered)
        {
            SetStateAction();
            triggered = true;
        }

        base.OnVoid();
    }

    protected override void DuringRewind()
    {
        rb.AddForce(Vector3.up * fallingSpeed);
    }

    protected override void EndRewind()
    {
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag)) print("le joueur est ded");
        else if (collision.gameObject.CompareTag(groundTag))
        {
            print("le sol je le touche en tant que block qui fall");
            SetStateVoid();
        }
    }
}
