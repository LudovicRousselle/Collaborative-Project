using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Corentin
public class Turret : MonoBehaviour
{
    public GameObject m_player { get; private set; }
    public SpawnProjectile spawnProjectile;
    [SerializeField] private GameObject m_parentFolder;
    [SerializeField] private List<GameObject> m_vfx = new List<GameObject>();
    private GameObject m_effectToSpawn;
    [SerializeField] private Alan m_subScript;
    [SerializeField] private Animator m_animator;

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
    public bool isOnSight = false;

    private void Start()
    {
        m_animator.enabled = false;
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
        if (m_player != null)
        {
            Vector3 direction = transform.position - m_player.transform.position;
            float angle = Vector3.Angle(transform.forward, -direction);

            if (direction.magnitude <= m_sightRange)
            {
                m_timeBeforeAttack = 5 * ((direction.magnitude - 1) / m_sightRange);

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
                        Debug.Log(hitMiddle.transform.name);
                        if (hitMiddle.transform.gameObject.CompareTag("Player"))
                        {
                            Debug.Log("WTF");
                            middleRayTouching = true;
                        }
                        else
                        {
                            Debug.Log("WHAT THE FUCKKK");
                            middleRayTouching = false;
                        }
                    }

                    if (Physics.Raycast(transform.position, ((m_player.transform.position + Vector3.up) - transform.position).normalized, out hitTop, m_sightRange))
                    {
                        if (hitTop.transform.gameObject.CompareTag("Player"))
                        {
                            topRayTouching = true;
                        }
                        else
                        {
                            topRayTouching = false;
                        }
                    }

                    if (Physics.Raycast(transform.position, ((m_player.transform.position - Vector3.up) - transform.position).normalized, out hitBot, m_sightRange))
                    {
                        if (hitBot.transform.gameObject.CompareTag("Player"))
                        {
                            botRayTouching = true;
                        }
                        else
                        {
                            botRayTouching = false;
                        }
                    }

                    if (middleRayTouching || topRayTouching || botRayTouching)
                    {
                        Debug.Log("Last - 4");
                        //FeedBack targetPLayer
                        targetingPlayer = true;

                        if (turret != null)
                        {
                            turret.LookAt(m_player.transform);
                            turret.Rotate(turret.rotation.x - 90, turret.rotation.y - 90, turret.rotation.z - 180);
                        }
                        else
                        {
                            transform.LookAt(m_player.transform);
                        }

                        m_loadingAttack += Time.deltaTime;
                        return true;
                    }
                    else
                    {
                        Debug.Log("Last - 3");
                        targetingPlayer = false;
                        m_loadingAttack = 0;
                        return false;
                    }
                }
                else
                {
                    Debug.Log("Last - 2");
                    targetingPlayer = false;
                    m_loadingAttack = 0;
                    return false;
                }
            }
            else
            {
                Debug.Log("Last - 1");
                targetingPlayer = false;
                m_loadingAttack = 0;
                return false;
            }
        }else
        {
            Debug.Log("Last");
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

    public void RobotDeath()
    {
        isDead = true;
        oneTime = true;

        if (m_subScript != null)
        {
            m_subScript.enabled = false;
        }

        if (m_animator.enabled == false)
        {
            m_animator.enabled = true;
        }

        m_animator.Play("Anim_Death", 0);

        Invoke("DeathVFX", 3.0f);

        //Destroy animation
        float delay = 3f;
        Destroy(m_parentFolder, delay);
    }

    public void DeathVFX()
    {
        GameObject vfxBoom;
        GameObject vfxSmoke;

        m_effectToSpawn = m_vfx[0];
        vfxBoom = Instantiate(m_effectToSpawn, transform.position, Quaternion.identity);
        m_effectToSpawn = m_vfx[1];
        vfxSmoke = Instantiate(m_effectToSpawn, transform.position, Quaternion.identity);
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
                    isOnSight = true;
                    Attack();
                }
            }else
            {
                isOnSight = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isOnSight = false;
            targetingPlayer = false;
            m_loadingAttack = 0;
        }
    }
}
