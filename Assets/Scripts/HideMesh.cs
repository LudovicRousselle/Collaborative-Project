using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Corentin
public class HideMesh : MonoBehaviour
{
    private MeshRenderer m_MeshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_MeshRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
