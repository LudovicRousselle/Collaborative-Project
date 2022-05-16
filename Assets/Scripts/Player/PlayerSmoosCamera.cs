using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmoosCamera : MonoBehaviour
{
    public static Vector3 offset = new Vector3(0f, 0f, -17f);
    [SerializeField] private Vector3 velocity = Vector3.zero;
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private Transform target;

    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
