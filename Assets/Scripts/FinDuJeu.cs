using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinDuJeu : RewindableObject
{

    bool oneTimeUse;

    // Start is called before the first frame update
    void Start()
    {
        oneTimeUse = false;
    }

    protected override void DuringRewind()
    {
        Debug.Log("Finishing the jeu");
            SceneManager.LoadScene(0);
    
    }

    
}
