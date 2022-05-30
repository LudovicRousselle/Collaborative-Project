using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Corentin
public class Turret : MonoBehaviour
{
    public GameObject m_player { get; private set; }
    public SpawnProjectile spawnProjectile;
    [SerializeField] private GameObject m_parentFolder;

    //Attack
    [SerializeField] private float m_sightRange;
    private float m_timeBeforeAttack = 5.0f;
    public bool targetingPlayer = false;
    private float m_loadingAttack = 0;

    private bool middleRayTouching = false;
    private bool topRayTouching = false;
    private bool botRayTouching = false;

    [SerializeField] private Transform turret;

    bool oneTime = false;
    bool isDead = false;

    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {

        if (!targetingPlayer && turret == null)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }else if (!targetingPlayer && turret != null)
        {
            //Debugging
            turret.rotation = new Quaternion(0, 0, 0, 0);

            //Go Back to Last position
            MoveToLastPosition();
        }

        if (isDead)
        {
            targetingPlayer = false;
        }
    }


    bool OnSight()
    {
        Vector3 direction = transform.position - m_player.transform.position;
        float angle = Vector3.Angle(transform.forward, -direction);

        if (direction.magnitude <= m_sightRange)
        {
            m_timeBeforeAttack = 5 * ((direction.magnitude - 1 )/ m_sightRange);

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

                    if (turret != null)
                    {
                        turret.LookAt(m_player.transform);
                        turret.Rotate(turret.rotation.x - 90, turret.rotation.y - 90, turret.rotation.z -180);
                    }
                    else
                    {
                        transform.LookAt(m_player.transform);
                    }

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
        if (m_loadingAttack >= m_timeBeforeAttack && m_loadingAttack >= 1.2f)
        {
            if (!oneTime)
            {
                //Attack
                spawnProjectile.SpawnVFX();

                isDead = true;
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
    
    private void MoveToLastPosition()
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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !isDead)
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
