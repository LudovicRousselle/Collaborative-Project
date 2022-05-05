using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFallingBlock : RewindableObject
{
    [SerializeField] private float fallingSpeed;
    [SerializeField] private FallingBlockTriggerZone triggerZone;

    private Rigidbody rb;
    private bool hasFallen;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected override void OnProceed()
    {
        rb.AddForce(-Vector3.up * fallingSpeed);
        //detecter le sol
    }

    protected override void OnVoid()
    {
        if (triggerZone.triggered)
        {
            SetStateProceed();
            hasFallen = true;
        }

        base.OnVoid();
    }
}
