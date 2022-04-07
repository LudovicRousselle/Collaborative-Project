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
    [SerializeField] private float m_timeBeforeAttack;
    private float m_loadingAttack = 0;


    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {

    }

    //Idle
    private void IdleAI()
    {

    }

    bool OnSight()
    {
        Vector3 direction = transform.position - m_player.transform.position;
        float angle = Vector3.Angle(transform.forward, -direction);

        if (angle < 30 && direction.magnitude <= m_sightRange)
        {
            m_loadingAttack += Time.deltaTime;
            return true;
        }
        else
        {
            m_loadingAttack = 0;
            return false;
        }
    }

    //Attack the Player
    private void Attack()
    {
        if (m_loadingAttack >= m_timeBeforeAttack)
        {
            m_player.SendMessage("Die");
        }
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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (OnSight())
            {
                Attack();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            m_loadingAttack = 0;
    }
}
