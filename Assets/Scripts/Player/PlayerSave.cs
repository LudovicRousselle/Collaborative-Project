using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Corentin
public class PlayerSave : MonoBehaviour
{
    private SaveManager m_saveManager;

    // Start is called before the first frame update
    void Start()
    {
        m_saveManager = SaveManager.instance;
        transform.position = m_saveManager.lastCheckPointPos;
    }

    // Update is called once per frame
    void Update()
    {
        //Debuging
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
