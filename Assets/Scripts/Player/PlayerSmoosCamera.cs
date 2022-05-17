using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmoosCamera : MonoBehaviour
{
    [SerializeField] public static Vector3 offset = new Vector3(0f, 0f, -17f);
    [SerializeField] private Vector3 velocity = Vector3.zero;
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private Transform target;
    [SerializeField] private Transform target2;
    [SerializeField] public static bool targeting;
    void Start()
    {
        targeting = true;
    }
    void Update()
    {
        if (targeting)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else if (!targeting)
        {
            Vector3 targetPosition = target2.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        
    }
}
