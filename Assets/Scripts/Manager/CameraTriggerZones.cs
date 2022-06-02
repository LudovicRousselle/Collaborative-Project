using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerZones : MonoBehaviour
{

    PlayerSmoosCamera cameraReference;



    // Start is called before the first frame update
    void Start()
    {
        cameraReference = FindObjectOfType<PlayerSmoosCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (cameraReference.currentTarget == transform)
            { return; }

            cameraReference.SwitchTarget(transform);
        }
    }
}
