using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alan : MonoBehaviour
{
    private const float LIMIT_PATROL = 5;

    [SerializeField] private Transform patrolFirstPos;
    [SerializeField] private Transform patrolSecondPos;
    private Vector3 m_targetPosition;
    private Vector3 m_targetRotation;
    private Direction m_direction;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        m_direction = Direction.SecondPos;
        m_targetPosition = patrolSecondPos.position;
    }

    // Update is called once per frame
    void Update()
    {

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, m_targetPosition) > 0.01f)
        {
            m_targetRotation = m_targetPosition - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_targetRotation), 5f * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, m_targetPosition, 5 * Time.deltaTime);
        }else
        {
            //Start WaitHere
            if (coroutine == null)
            {
                coroutine = WaitHere();
                StartCoroutine(coroutine);
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
