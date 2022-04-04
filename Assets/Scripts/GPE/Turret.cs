using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //Reference
    private Animator m_Animator;
    private GameObject m_player;

    //Attack
    [SerializeField] private float m_sightRange;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Attack();
    }

    //Idle
    private void IdleAI()
    {

    }

    //Attack the Player
    private void Attack()
    {
        Vector3 direction = transform.position - m_player.transform.position;
        float angle = Vector3.Angle(transform.forward, -direction);
        //Debug.Log(angle + " degrees");

        if (angle < 30 && direction.magnitude <= m_sightRange)
        {
            Debug.Log("On Sight");
            //EnableBackstab(true);
        }
        else
        {
            Debug.Log("Out Of Sight");
            //EnableBackstab(false);
        }
    }

    private void DestroyEnemy()
    {

    }

    private void OnDrawGizmosSelected()
    {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_sightRange);

            Vector3 dirUp = new Vector3(0, 1, 1 / Mathf.Tan(30 * Mathf.Deg2Rad)).normalized * m_sightRange;
            Vector3 dirDown = new Vector3(0, -1, 1 / Mathf.Tan(30 * Mathf.Deg2Rad)).normalized * m_sightRange;

            Debug.DrawRay(transform.position, transform.TransformDirection(dirUp), Color.red);
            Debug.DrawRay(transform.position, transform.TransformDirection(dirDown), Color.red);
    }
}
