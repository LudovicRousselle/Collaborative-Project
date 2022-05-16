using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlockTriggerZone : MonoBehaviour
{
    private NewFallingBlock fallingBlock;
    public bool triggered = false;

    private void Start()
    {
        fallingBlock = GetComponentInParent<NewFallingBlock>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(fallingBlock.targetTag))
        {
            triggered = true;
        }
    }
}
