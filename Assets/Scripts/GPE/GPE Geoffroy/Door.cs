using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float travelDistUp = 2f;
    [SerializeField] private bool startOpened = false;

    private float baseYPos;

    private bool isMoving = false;
    private bool isOpened = false;

    void Start()
    {
        baseYPos = transform.position.y;
        travelDistUp += transform.position.y;

        if (startOpened) OnInteract();
    }

    void Update()
    {
        if (isMoving) Move();
    }

    public void OnInteract()
    {
        if (isMoving) return;

        print("porte la porte");

        isMoving = true;
    }

    private void Move()
    {
        print("la porte se mouvoie");

        if (!isOpened)
        {
            print("en plus de se mouvoir, elle se mouvoie joliement vers le haut");
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, travelDistUp, transform.position.z), speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, new Vector3(transform.position.x, travelDistUp, transform.position.z)) < 0.01f)
            {
                isMoving = false;
                isOpened = true;
            }
        }
        else if (isOpened)
        {
            print("la porte va vers le bas");
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, baseYPos, transform.position.z), speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, new Vector3(transform.position.x, baseYPos, transform.position.z)) < 0.01f)
            {
                isMoving = false;
                isOpened = false;
            }
        }
    }
}
