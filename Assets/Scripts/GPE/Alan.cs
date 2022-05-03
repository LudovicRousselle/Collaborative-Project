using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Corentin
public class Alan : MonoBehaviour
{
    [SerializeField] private Transform patrolFirstPos;
    [SerializeField] private Transform patrolSecondPos;
    [SerializeField] private float m_speed = 2f;
    [SerializeField] private float m_speedRotation = 5f;
    private Vector3 m_targetPosition;
    private Vector3 m_targetRotation;
    private Direction m_direction;
    private IEnumerator coroutine;

    private Turret m_turret;
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_turret = GetComponentInChildren<Turret>();
        m_animator = GetComponent<Animator>();
        m_direction = Direction.SecondPos;
        m_targetPosition = patrolSecondPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_turret.targetingPlayer)
        {
            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, m_targetPosition) > 0.01f)
            {
                m_animator.Play("Anim_Walk", 0);
                transform.position = Vector3.MoveTowards(transform.position, m_targetPosition, m_speed * Time.deltaTime);
            }
            else
            {
                //Start WaitHere
                if (coroutine == null)
                {
                    coroutine = WaitHere();
                    StartCoroutine(coroutine);
                }
            }
        }else
        {
            m_animator.Play("Anim_Spotted", 0);
        }
    }

    private IEnumerator WaitHere()
    {
        int random = Random.Range(1, 3);
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
        Debug.Log(random);
        yield return new WaitForSeconds(3f);
        m_animator.Play("Anim_TurnBack", 0);
        transform.Rotate(0, 180, 0);
        yield return new WaitForSeconds(2f);
        NextDirection(m_direction);
        coroutine = null;
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

    enum Direction { FirstPos, SecondPos}
}
