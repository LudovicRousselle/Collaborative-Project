using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlockTriggerZone : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";

    public bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            triggered = true;
        }
    }
}
