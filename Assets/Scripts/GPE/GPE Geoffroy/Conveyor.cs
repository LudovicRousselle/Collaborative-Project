using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : RewindableObject
{
    [SerializeField] private bool startDirectionRight = true;
    public float speed = 5f;
    public bool isOn;

    private List<GameObject> targetList;

    private void Start()
    {
        targetList = new List<GameObject>();
        if (!startDirectionRight) speed *= -1;

        SetStateAction();
    }

    protected override void DoAction()
    {
        if (isOn)
        {
            if (targetList.Count == 0) return;

            foreach (GameObject currentGameObject in targetList)
            {
                currentGameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * speed);
            }
        }
        
    }

    protected override void DuringRewind()
    {
        if (isOn)
        {
            if (targetList.Count == 0) return;

            foreach (GameObject currentGameObject in targetList)
            {
                currentGameObject.GetComponent<Rigidbody>().AddForce(-Vector3.right * speed);
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("SBlock")) 
            targetList.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        if(targetList.Contains(collision.gameObject)) 
            targetList.Remove(collision.gameObject);
    }
}