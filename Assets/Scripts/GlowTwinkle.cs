using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowTwinkle : MonoBehaviour
{
    [SerializeField] private MeshRenderer m_renderer;
    private Material m_mat;

    public float intensity;

    private void Start()
    {
        m_mat = m_renderer.materials[1];
        m_mat.EnableKeyword("_EMISSION");


    }

    private void Update()
    {
        intensity = Mathf.Sin(Time.time * 0.5f);
        intensity = Mathf.Clamp(intensity, 0, 1);

        m_mat.SetColor("_EmissionColor", Color.red * intensity);
    }
}
