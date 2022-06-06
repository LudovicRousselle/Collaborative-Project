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

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
    }
}
