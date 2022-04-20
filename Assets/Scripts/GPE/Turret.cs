using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Corentin
public class Turret : MonoBehaviour
{
    //Reference
    [SerializeField] private Animation m_Idle;
    private GameObject m_player;
    [SerializeField] private GameObject m_parentFolder;

    //Attack
    [SerializeField] private float m_sightRange;
    [SerializeField] private float m_timeBeforeAttack;
    public bool targetingPlayer = false;
    private float m_loadingAttack = 0;

    private bool middleRayTouching = false;
    private bool topRayTouching = false;
    private bool botRayTouching = false;

    bool oneTime = false;

    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            RobotDeath();
        }
    }


    bool OnSight()
    {
        Vector3 direction = transform.position - m_player.transform.position;
        float angle = Vector3.Angle(transform.forward, -direction);

        if (direction.magnitude <= m_sightRange)
        {

            if (angle < 30)
            {
                RaycastHit hitMiddle;
                RaycastHit hitTop;
                RaycastHit hitBot;

                Debug.DrawRay(transform.position, (m_player.transform.position - transform.position).normalized * m_sightRange, Color.yellow);
                Debug.DrawRay(transform.position, ((m_player.transform.position + Vector3.up) - transform.position).normalized * m_sightRange, Color.yellow);
                Debug.DrawRay(transform.position, ((m_player.transform.position - Vector3.up) - transform.position).normalized * m_sightRange, Color.yellow);

                if (Physics.Raycast(transform.position, (m_player.transform.position - transform.position).normalized, out hitMiddle, m_sightRange))
                {
                    //&& Physics.Raycast(transform.position, ((m_player.transform.position + Vector3.up) - transform.position).normalized, out hitTop, m_sightRange)
                    //&& Physics.Raycast(transform.position, ((m_player.transform.position - Vector3.up) - transform.position).normalized, out hitBot, m_sightRange)
                    // || hitTop.transform.gameObject.CompareTag("Player") || hitBot.transform.gameObject.CompareTag("Player")
                    if (hitMiddle.transform.gameObject.CompareTag("Player"))
                    {
                        middleRayTouching = true;
                    }else
                    {
                        middleRayTouching = false;
                    }
                }
                
                if (Physics.Raycast(transform.position, ((m_player.transform.position + Vector3.up) - transform.position).normalized, out hitTop, m_sightRange))
                {
                    if (hitTop.transform.gameObject.CompareTag("Player"))
                    {
                        topRayTouching = true;
                    }else
                    {
                        topRayTouching = false;
                    }
                }
                
                if (Physics.Raycast(transform.position, ((m_player.transform.position - Vector3.up) - transform.position).normalized, out hitBot, m_sightRange))
                {
                    if (hitBot.transform.gameObject.CompareTag("Player"))
                    {
                        botRayTouching = true;
                    }else
                    {
                        botRayTouching = false;
                    }
                }

                if (middleRayTouching || topRayTouching || botRayTouching)
                {
                    //FeedBack targetPLayer
                    targetingPlayer = true;
                    transform.LookAt(m_player.transform);

                    m_loadingAttack += Time.deltaTime;
                    return true;
                }else
                {
                    targetingPlayer = false;
                    m_loadingAttack = 0;
                    return false;
                }
            }
            else
            {
                targetingPlayer = false;
                m_loadingAttack = 0;
                return false;
            }
        }else
        {
            targetingPlayer = false;
            m_loadingAttack = 0;
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

    private void RobotDeath()
    {
        //Destroy animation
        float delay = 1f;
        Destroy(m_parentFolder, delay);
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
            targetingPlayer = false;
            m_loadingAttack = 0;
        }
    }
}
