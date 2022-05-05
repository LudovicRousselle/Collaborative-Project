using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : RewindableObject
{
    [SerializeField] private bool startDirectionRight = true;
    [SerializeField] private float speed = 5f;

    private bool goingRight = true;
    private List<GameObject> targetList;

    private void Start()
    {
        goingRight = startDirectionRight;
        isRewinded = !startDirectionRight;

        targetList = new List<GameObject>();

        SetStateVoid();
    }

    private void Behavior()
    {
        if (targetList.Count == 0) return;

        foreach (GameObject currentGameObject in targetList)
        {
            if (goingRight)
            {
                if (currentGameObject.CompareTag("Player") || currentGameObject.CompareTag("SBlock"))
                    currentGameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * speed);
            }
            else
            {
                if (currentGameObject.CompareTag("Player") || currentGameObject.CompareTag("SBlock"))
                    currentGameObject.GetComponent<Rigidbody>().AddForce(-Vector3.right * speed);
            }
        }
    }

    protected override void OnProceed()
    {
        goingRight = true;
        base.OnProceed();
    }

    protected override void OnRewind()
    {
        goingRight = false;
        base.OnRewind();
    }

    protected override void OnVoid()
    {
        Behavior();
    }

    private void OnCollisionEnter(Collision collision)
    {
        targetList.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        targetList.Remove(collision.gameObject);
    }
}