using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private SaveManager m_saveManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_saveManager.lastCheckPointPos = transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_saveManager = SaveManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
