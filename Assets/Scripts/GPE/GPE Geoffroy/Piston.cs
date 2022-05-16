using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : RewindableObject
{
    [Header("Parts")]
    [SerializeField] private GameObject UpBox;
    [SerializeField] private Transform UpBoxPeakTransform;
    [SerializeField] private Transform UpBoxMiddleTransform;
    [SerializeField] private Transform UpBoxLowestTransform;
    [SerializeField] private GameObject UpPiston;
    [SerializeField] private GameObject LowPiston;

    [SerializeField] private float speed = 1;
    [SerializeField] private bool isClosedByDefault = false;

    private Vector3 UpBoxPeakPos;
    private Vector3 UpBoxMiddlePos;
    private Vector3 UpBoxLowestPos;

    private Vector3 endPos;
    private Vector3 startPos;

    private void Start()
    {
        UpBoxPeakPos = UpBoxPeakTransform.position;
        UpBoxMiddlePos = UpBoxMiddleTransform.position;
        UpBoxLowestPos = UpBoxLowestTransform.position;

        endPos = UpBoxLowestPos;
        startPos = UpBoxPeakPos;

        if (isClosedByDefault)
        {
            UpBox.transform.position = UpBoxLowestPos;
            startPos = UpBoxLowestPos;
            endPos = UpBoxPeakPos;
        }

        SetStateAction();
    }

    protected override void DoAction()
    {
        if (Vector3.Distance(UpBox.transform.position, UpBoxMiddlePos) > 0.01f)
        {
            UpBox.transform.position = Vector3.MoveTowards(UpBox.transform.position, UpBoxMiddlePos, speed * Time.deltaTime);
        }
        else if(Vector3.Distance(UpBox.transform.position, endPos) > 0.01f)
        {
            UpBox.transform.position = Vector3.MoveTowards(UpBox.transform.position, endPos, speed * Time.deltaTime);
        }
        
        base.DoAction();
    }
}
