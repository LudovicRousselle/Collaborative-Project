using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //Reference
    [SerializeField] private Animation m_Idle;
    private GameObject m_player;

    //Attack
    [SerializeField] private float m_sightRange;
    [SerializeField] private float m_timeBeforeAttack;
    private float m_loadingAttack = 0;

    bool oneTime = false;

    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {

    }


    bool OnSight()
    {
        Vector3 direction = transform.position - m_player.transform.position;
        float angle = Vector3.Angle(transform.forward, -direction);

        if (direction.magnitude <= m_sightRange)
        {
            transform.LookAt(m_player.transform);

            if (angle < 30)
            {

                m_loadingAttack += Time.deltaTime;
                return true;
            }
            else
            {
                m_loadingAttack = 0;
                return false;
            }
        }else
        {
            return false;
        }

    }

    //Attack the Player
    private void Attack()
    {
        if (m_loadingAttack >= m_timeBeforeAttack)
        {
            if (!oneTime)
            {
                m_player.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
                Debug.Log("Player died");

                oneTime = true;
            }
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
                if (m_loadingAttack > m_timeBeforeAttack)
                {
                    Attack();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_loadingAttack = 0;
        }
    }
}
