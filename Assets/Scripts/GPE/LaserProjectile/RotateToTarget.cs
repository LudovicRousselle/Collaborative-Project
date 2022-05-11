using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    public Transform target;
    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateTo();
    }

    private void RotateTo()
    {
        Vector3 direction = target.position - transform.position;
        rotation = Quaternion.LookRotation(direction);
        gameObject.transform.localRotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, 1);
    }

    public Quaternion GetRotation()
    {
        return rotation;
    }
}
