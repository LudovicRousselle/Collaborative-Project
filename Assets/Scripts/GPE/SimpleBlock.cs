using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBlock : MonoBehaviour
{
    [SerializeField] private Killzone killzone;

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (killzone.gameObject.activeSelf) killzone.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!killzone.gameObject.activeSelf) killzone.gameObject.SetActive(true);
        }
    }
}
