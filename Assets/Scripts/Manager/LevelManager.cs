using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    #region singleton
    public static LevelManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private IEnumerator m_reloadScene;
    private IEnumerator m_nextScene;
    [SerializeField] private GameObject myLittleInGameCanvas;

    //Transitions
    [SerializeField] private Image m_transition;
    public bool m_Fading = false;
    public bool nextLevel = false;

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene);

        m_transition.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance.isDead == true)
        {
            if (m_reloadScene == null)
            {
                m_reloadScene = ReloadScene();
                StartCoroutine(m_reloadScene);
            }
        }

        if (nextLevel == true)
        {
            if (m_nextScene == null)
            {
                m_nextScene = NextScene();
                StartCoroutine(m_nextScene);
            }
        }

        //If the toggle returns true, fade in the Image
        if (m_Fading == true)
        {
            //Fully fade in Image (1) with the duration of 2
            m_transition.CrossFadeAlpha(1, .25f, false);
        }
        //If the toggle is false, fade out to nothing (0) the Image with a duration of 2
        if (m_Fading == false)
        {
            m_transition.CrossFadeAlpha(0, .25f, false);
        }
    }

    IEnumerator ReloadScene()
    {
        DontDestroyOnLoad(instance);
        DontDestroyOnLoad(myLittleInGameCanvas);

        m_Fading = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        m_Fading = false;
        m_reloadScene = null;
    }

    IEnumerator NextScene()
    {
        m_Fading = true;
        yield return new WaitForSeconds(2f);

        Scene scene = SceneManager.GetActiveScene();
        int buildIndex = scene.buildIndex;

        SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(myLittleInGameCanvas, SceneManager.GetActiveScene());
        SceneManager.LoadScene(buildIndex + 1);

        nextLevel = false;
        m_Fading = false;
        m_nextScene = null;
    }
}
