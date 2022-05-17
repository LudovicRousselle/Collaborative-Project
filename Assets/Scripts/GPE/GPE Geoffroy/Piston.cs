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
    [SerializeField] private GameObject BottomBox;
    [SerializeField] private GameObject UpPiston;
    [SerializeField] private GameObject LowPiston;

    [Header("BaseParams")]
    [SerializeField] private float speed = 1;
    [SerializeField] private bool isClosedByDefault = false;

    [Header("Useless shit but it's there if you want")]
    [SerializeField] private float animPauseTime = 1;

    private Vector3 UpBoxPeakPos;
    private Vector3 UpBoxMiddlePos;
    private Vector3 UpBoxLowestPos;

    private Vector3 endPos;
    private Vector3 startPos;

    private bool hasReachedMiddle = false;

    private float customCounter;

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
            UpPiston.transform.position = BottomBox.transform.position;
            LowPiston.transform.position = BottomBox.transform.position;
            startPos = UpBoxLowestPos;
            endPos = UpBoxPeakPos;
        }

        customCounter = 0;

        SetStateVoid();
    }

    protected override void DoAction()
    {
        if (!isClosedByDefault)
        {
            UnFold(startPos);
        }
        else Fold(startPos);

        base.DoAction();
    }

    protected override void DuringRewind()
    {
        base.DuringRewind();

        if (!isClosedByDefault)
        {
            Fold(endPos);
        }
        else UnFold(endPos);
    }

    protected override void EndRewind()
    {
        base.EndRewind();

        SetStateAction();
    }

    private void Fold(Vector3 target)
    {
        if (!hasReachedMiddle && Vector3.Distance(UpBox.transform.position, UpBoxMiddlePos) > 0.02f)
        {
            UpBox.transform.position = Vector3.MoveTowards(UpBox.transform.position, UpBoxMiddlePos, speed * Time.deltaTime);
            UpPiston.transform.position += new Vector3(0, -speed * Time.deltaTime);
        }
        else if (customCounter < animPauseTime)
        {
            hasReachedMiddle = true;
            customCounter += Time.deltaTime;
        }
        else if (Vector3.Distance(UpBox.transform.position, target) > 0.005f)
        {
            UpBox.transform.position = Vector3.MoveTowards(UpBox.transform.position, target, speed * Time.deltaTime);
            UpPiston.transform.position += new Vector3(0, -speed * Time.deltaTime);
            LowPiston.transform.position += new Vector3(0, -speed * Time.deltaTime);
        }
        else
        {
            if (isClosedByDefault) SetStateVoid();
        }

        print("Je me plie sans être du papier puisqu'en effet je me trouve être un piston");
    }

    private void UnFold(Vector3 target)
    {
        if (!hasReachedMiddle && Vector3.Distance(UpBox.transform.position, UpBoxMiddlePos) > 0.005f)
        {
            UpBox.transform.position = Vector3.MoveTowards(UpBox.transform.position, UpBoxMiddlePos, speed * Time.deltaTime);
            UpPiston.transform.position += new Vector3(0, speed * Time.deltaTime);
            LowPiston.transform.position += new Vector3(0, speed * 1.015f * Time.deltaTime);
        }
        else if (customCounter < animPauseTime)
        {
            hasReachedMiddle = true;
            customCounter += Time.deltaTime;
        }
        else if (Vector3.Distance(UpBox.transform.position, target) > 0.005f)
        {
            UpBox.transform.position = Vector3.MoveTowards(UpBox.transform.position, target, speed * Time.deltaTime);
            UpPiston.transform.position += new Vector3(0, speed * Time.deltaTime);
        }
        else
        {
            if (!isClosedByDefault) SetStateVoid();
        }

        print("J'me déplie wola.  -Le Piston");
    }

    protected override void SetStateAction()
    {
        base.SetStateAction();
        hasReachedMiddle = false;
        customCounter = 0;
    }

    protected override void SetStateRewind()
    {
        base.SetStateRewind();
        hasReachedMiddle = false;
        customCounter = 0;
    }
}
