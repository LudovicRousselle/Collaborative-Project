using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    [SerializeField] private bool startOpened = false;
    [SerializeField] private List<GameObject> doors = new List<GameObject>();

    public bool isOpened = false;

    void Start()
    {
        if (startOpened) OnInteract();
    }

    public void OnInteract()
    {
        if (isOpened)
        {
            isOpened = false;

            foreach (var item in doors)
            {
                item.gameObject.SetActive(true);
            }
        }
        else if (!isOpened)
        {
            isOpened = true;

            foreach (var item in doors)
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    
}
