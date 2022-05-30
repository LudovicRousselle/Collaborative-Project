using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Corentin
public class Alan : MonoBehaviour
{
    //References
    private Turret m_turret;
    private Animator m_animator;
    [SerializeField] private Transform m_leftArm;

    //Patroling and moving
    [SerializeField] private Transform patrolFirstPos;
    [SerializeField] private Transform patrolSecondPos;
    [SerializeField] private float m_speed = 2f;
    [SerializeField] private float m_speedRotation = 5f;
    private Vector3 m_targetPosition;
    private Direction m_direction;

    //Animations
    private IEnumerator m_waitHere;
    private IEnumerator m_spotted;
    private IEnumerator m_turnBack;
    private float m_smoothAnimTransition = 0;
    private AudioSource m_audioSource;

    //Sound
    [SerializeField] private AudioClip[] m_stepsClip;
    [SerializeField] private AudioClip m_turnClip;

    // Start is called before the first frame update
    void Start()
    {
        m_turret = GetComponentInChildren<Turret>();
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
        m_direction = Direction.SecondPos;
        m_targetPosition = patrolSecondPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_turret.targetingPlayer)
        {
            if (m_smoothAnimTransition <= 1.0f)
            {
                m_smoothAnimTransition += Time.deltaTime;
            }

            if (m_smoothAnimTransition >= 1.0f)
            {
                if (m_spotted != null)
                {
                    StopCoroutine(m_spotted);
                    m_animator.enabled = true;
                    m_spotted = null;
                }

                // Check if the position of the cube and sphere are approximately equal.
                if (Vector3.Distance(transform.position, m_targetPosition) > 0.01f)
                {
                    m_animator.Play("Anim_Walk", 0);
                    transform.position = Vector3.MoveTowards(transform.position, m_targetPosition, m_speed * Time.deltaTime);
                }
                else
                {
                    //Start WaitHere
                    if (m_waitHere == null)
                    {
                        m_waitHere = WaitHere();
                        StartCoroutine(m_waitHere);
                    }
                }
            }
        }else
        {
            if (m_spotted == null)
            {
                if (m_waitHere != null)
                {
                    StopCoroutine(m_waitHere);
                    m_waitHere = null;
                }

                m_spotted = SpottedAnimation();
                StartCoroutine(m_spotted);
            }

            GameObject player = m_turret.m_player;
            m_leftArm.LookAt(player.transform);
            m_leftArm.Rotate(m_leftArm.rotation.x, m_leftArm.rotation.y + 90, m_leftArm.rotation.z);
        }
    }

    private IEnumerator WaitHere()
    {
        int random = Random.Range(1, 4);
        switch (random)
        {
            case 1:
                m_animator.Play("Anim_Idle1", 0);
                break;
            case 2:
                m_animator.Play("Anim_Idle2", 0);
                break;
            case 3:
                m_animator.Play("Anim_Idle3", 0);
                break;
        }

        yield return new WaitForSeconds(3f);

        if (m_turnBack == null)
        {
            m_turnBack = TurnBack();
            StartCoroutine(m_turnBack);
        }
    }

    private IEnumerator TurnBack()
    {
        m_animator.Play("Anim_TurnBack", 0);
        transform.Rotate(0, 180, 0);
        yield return new WaitForSeconds(2f);
        NextDirection(m_direction);


        m_waitHere = null;
        m_turnBack = null;

        StopAllCoroutines();
    }

    private IEnumerator SpottedAnimation()
    {
        //Play Spotted animation
        m_smoothAnimTransition = 0;
        m_animator.Play("Anim_Spotted", 0);

        yield return new WaitForSeconds(1.25f);
        m_animator.enabled = false;
    }

    private void NextDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.FirstPos:
                m_targetPosition = patrolSecondPos.position;
                m_direction = Direction.SecondPos;
                break;
            case Direction.SecondPos:
                m_targetPosition = patrolFirstPos.position;
                m_direction = Direction.FirstPos;
                break;
        }
    }

    public void FootStep()
    {
        int random = Random.Range(0, 3);
        float randomPitch = Random.Range(0.8f, 1.01f);

        m_audioSource.pitch = randomPitch;
        m_audioSource.PlayOneShot(m_stepsClip[random]);
    }

    public void TurnSound()
    {
        m_audioSource.pitch = 1.0f;
        m_audioSource.PlayOneShot(m_turnClip, 0.8f);
    }

    enum Direction { FirstPos, SecondPos}
}
