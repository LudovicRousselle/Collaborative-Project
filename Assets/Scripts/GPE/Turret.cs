using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //Reference
    private Animator m_Animator;
    private GameObject m_player;

    //Attack
    [SerializeField] private float m_sightRange;


    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
    }

    //Idle
    private void IdleAI()
    {

    }

    //Attack the Player
    private void Attack()
    {

    }

    private void DestroyEnemy()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_sightRange);
    }
}
