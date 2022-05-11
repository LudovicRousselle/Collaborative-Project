using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    [SerializeField] private GameObject m_firePoint;
    [SerializeField] private List<GameObject> m_vfx = new List<GameObject>();

    [SerializeField] private RotateToTarget m_rotateToTarget;

    private GameObject m_effectToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        m_effectToSpawn = m_vfx[0];
    }

    public void SpawnVFX()
    {
        GameObject vfx;

        if (m_firePoint != null)
        {
            vfx = Instantiate(m_effectToSpawn, m_firePoint.transform.position, Quaternion.identity);
            if (m_rotateToTarget != null)
            {
                vfx.transform.localRotation = m_rotateToTarget.GetRotation();
            }
        }else
        {
            Debug.Log("No Fire Point");
        }
    }
}
