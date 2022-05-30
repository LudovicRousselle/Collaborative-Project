using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

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
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Reloaded");
    }
}
