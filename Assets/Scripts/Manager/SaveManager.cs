using System.Collections;
using System.Collections.Generic;
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
    }
    #endregion

    //PlayerPosition
    [SerializeField] private Transform m_startLevelPos;
    public Vector3 lastCheckPointPos;

    // Start is called before the first frame update
    void Start()
    {
        lastCheckPointPos = m_startLevelPos.position;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
