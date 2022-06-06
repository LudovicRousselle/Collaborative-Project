using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    #region singleton
    public static MusicManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_music;

    private void Start()
    {
        m_audioSource.clip = m_music;
        m_audioSource.Play();
    }

    private void Update()
    {
        if (!m_audioSource.isPlaying)
        {
            m_audioSource.Play();
        }
    }
}
