using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvingPlatform : MonoBehaviour
{
    [SerializeField] private Transform movingPlatform;
    [SerializeField] private Transform position1;
    [SerializeField] private Transform position2;
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private string currentsState;
    [SerializeField] private float smooth;
    [SerializeField] private float resetTime;
    // Start is called before the first frame update
    void Start()
    {
        ChangeTarget();
    }
   
    private void FixedUpdate()
    {
        movingPlatform.position = Vector3.Lerp(movingPlatform.position, newPosition, smooth * Time.deltaTime);
    }
    private void ChangeTarget()
    {
        if (currentsState == "MtP1")
        {
            currentsState = "MtP2";
            newPosition = position2.position;
        }
        else if (currentsState == "MtP2")
        {
            currentsState = "MtP1";
            newPosition = position1.position;
        }
        else if (currentsState == "")
        {
            currentsState = "MtP2";
            newPosition = position2.position;
        }
        Invoke("ChangeTarget", resetTime);
    }
    
}
