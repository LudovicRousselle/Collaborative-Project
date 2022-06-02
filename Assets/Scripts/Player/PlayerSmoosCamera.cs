using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmoosCamera : MonoBehaviour
{
    [SerializeField] Vector3 offsetToPlayerPos;
    Vector3 velocity = Vector3.zero;
    [SerializeField] float smoothTime = 0.25f;
    [SerializeField] Transform targetPlayerGO;

    // List of the objects that contain the trigger zones
    [SerializeField] GameObject[] fixedPositions;

    Vector3 currentTargetPosition;
    public Transform currentTarget;

    [Header("Should the camera target the player or the fixed positions")]
    [SerializeField] bool shouldTargetPlayer;


    // A 2-s timer between each target switch to avoid jitter if the player moves rooms too fast
    float timerBetweenSwitches;


    private void Start()
    {
        timerBetweenSwitches = 0;    
    }

    void Update()
    {
        if (shouldTargetPlayer)
        {
            // Compute the position the camera should go to
            currentTargetPosition = targetPlayerGO.position + offsetToPlayerPos;

            // Move smoothly
            transform.position = Vector3.SmoothDamp(transform.position, currentTargetPosition, ref velocity, smoothTime);
        }
        else if (!shouldTargetPlayer)
        {
            // No offset if going to the trigger zones
            currentTargetPosition = currentTarget.position;


            transform.position = Vector3.SmoothDamp(transform.position, currentTargetPosition, ref velocity, smoothTime);
        }

        if (timerBetweenSwitches >= 0)
        {
            timerBetweenSwitches -= Time.deltaTime;
        }
    }


    public void SwitchTarget(Transform newTarget)
    {
        if (timerBetweenSwitches > 0)
        { return; }

        currentTarget = newTarget;
        timerBetweenSwitches = 1f;
    }
}
