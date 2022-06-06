using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip[] m_Clip;

    void PlayJump()
    {
        m_AudioSource.pitch = 1;
        m_AudioSource.PlayOneShot(m_Clip[0], 1f);
    }

    void PlayLanding()
    {
        m_AudioSource.pitch = 1;
        m_AudioSource.PlayOneShot(m_Clip[1], 1f);
    }

    void PlayFootStep()
    {
        int random = Random.Range(0, 3);
        float randomPitch = Random.Range(0.8f, 1.01f);

        m_AudioSource.pitch = randomPitch;
        m_AudioSource.PlayOneShot(m_Clip[2]);
    }

    void PlayRewind()
    {
        int random = Random.Range(0, 3);
        float randomPitch = Random.Range(0.8f, 1.01f);

        m_AudioSource.pitch = randomPitch;
        m_AudioSource.PlayOneShot(m_Clip[3], 1f);
    }


}
