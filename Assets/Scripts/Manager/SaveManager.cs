using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    #region singleton
    public static SaveManager instance;
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

        lastCheckPointPos = m_startLevelPos.position;
    }
    #endregion

    //PlayerPosition
    [SerializeField] private Transform m_startLevelPos;
    public Vector3 lastCheckPointPos;

    private IEnumerator m_coroutine;

    //Transitions
    [SerializeField] private Image m_transition;
    public bool m_Fading = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance == null)
        {
            if (m_coroutine == null)
            {
                m_coroutine = ReloadScene();
                StartCoroutine(m_coroutine);
            }
        }

        //If the toggle returns true, fade in the Image
        if (m_Fading == true)
        {
            //Fully fade in Image (1) with the duration of 2
            m_transition.CrossFadeAlpha(1, .5f, false);
        }
        //If the toggle is false, fade out to nothing (0) the Image with a duration of 2
        if (m_Fading == false)
        {
            m_transition.CrossFadeAlpha(0, .5f, false);
        }
    }

    IEnumerator ReloadScene()
    {
        m_Fading = true;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Reloaded");
    }
}
