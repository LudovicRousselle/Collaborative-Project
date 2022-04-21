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

    // Start is called before the first frame update
    void Start()
    {
        m_turret = GetComponentInChildren<Turret>();
        m_direction = Direction.SecondPos;
        m_targetPosition = patrolSecondPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        m_targetRotation = m_targetPosition - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_targetRotation), m_speedRotation * Time.deltaTime);

        if (!m_turret.targetingPlayer)
        {
            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, m_targetPosition) > 0.01f)
            {
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
        }
    }

    private IEnumerator WaitHere()
    {
        yield return new WaitForSeconds(5f);
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
