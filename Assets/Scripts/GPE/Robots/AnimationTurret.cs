using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTurret : MonoBehaviour
{
    [SerializeField] private Turret m_turret;

    public float rotationSpeed = 0.5f;

    bool rotating;

    Quaternion m_startRotation;

    private void Start()
    {
        m_startRotation = transform.rotation;
    }

    void Update()
    {
        if (!m_turret.isOnSight && !rotating)
        {
            if (transform.rotation == m_startRotation)
            {
                StartCoroutine(Rotate90());
            }else
            {
                StartCoroutine(GoBackWhereYouFromBitch());
            }
        }else if(m_turret.isOnSight)
        {
            StopAllCoroutines();

            rotating = false;
        }
    }
    IEnumerator Rotate90()
    {
        float minus = 1;

        if (transform.localRotation.eulerAngles.z > 0)
        {
            minus = -1;
        }

        rotating = true;

        float timeElapsed = 0;

        Quaternion veryStartRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 20 * minus, 0);

        while (timeElapsed < (0.75f + rotationSpeed))
        {
            transform.rotation = Quaternion.Slerp(veryStartRotation, targetRotation, timeElapsed / (0.75f + rotationSpeed));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        timeElapsed = 0;

        Quaternion startRotation = transform.rotation;
        targetRotation = transform.rotation * Quaternion.Euler(0, -40 * minus, 0);

        while (timeElapsed < rotationSpeed)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / rotationSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        timeElapsed = 0;

        startRotation = transform.rotation;
        targetRotation = transform.rotation * Quaternion.Euler(0, 20 * minus, 0);

        while (timeElapsed < (0.75f + rotationSpeed) && transform.localRotation.eulerAngles.y != 0)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / (0.75f + rotationSpeed));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        transform.rotation = veryStartRotation;

        StartCoroutine(Rotate180());
        StopCoroutine(Rotate90());
    }
    IEnumerator Rotate180()
    {
        float timeElapsed = 0;

        Quaternion veryStartRotation = transform.rotation;
        Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, 0, 180);

        while (timeElapsed < (rotationSpeed) && transform.localRotation.eulerAngles.z != 180)
        {
            transform.rotation = Quaternion.Slerp(veryStartRotation, targetRotation, timeElapsed / (rotationSpeed));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(Rotate90());
        StopCoroutine(Rotate180());
    }

    IEnumerator GoBackWhereYouFromBitch()
    {
        rotating = true;

        transform.rotation = m_startRotation;
        yield return null;

        rotating = false;

        //NOT WORKING LEL
        //float timeElapsed = 0;

        //float mXAngle = m_startRotation.x;
        //float mYAngle = m_startRotation.y;
        //float mZAngle = m_startRotation.z;

        //Quaternion veryStartRotation = transform.rotation;
        //Quaternion targetRotation = transform.rotation * Quaternion.Euler(mXAngle, mYAngle, mZAngle);

        //while (timeElapsed < (0.75f + rotationSpeed))
        //{
        //    transform.rotation = Quaternion.Slerp(veryStartRotation, targetRotation, timeElapsed / (rotationSpeed));
        //    timeElapsed += Time.deltaTime;
        //    yield return null;
        //}

        //transform.rotation = m_startRotation;

    }
}
