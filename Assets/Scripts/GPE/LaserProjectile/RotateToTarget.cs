using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    private Transform m_target;
    private Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RotateTo();
    }

    private void RotateTo()
    {
        if (m_target != null)
        {
            Vector3 direction = m_target.position - transform.position;
            rotation = Quaternion.LookRotation(direction);
            gameObject.transform.localRotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, 1);
        }
    }

    public Quaternion GetRotation()
    {
        return rotation;
    }
}
