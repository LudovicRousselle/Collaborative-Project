using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTurret : MonoBehaviour
{
    [SerializeField] private Turret m_turret;

    private void Update()
    {
        if (m_turret.isOnSight)
        {
            IdleAnimation();
        }
    }

    void IdleAnimation()
    {

    }
}
