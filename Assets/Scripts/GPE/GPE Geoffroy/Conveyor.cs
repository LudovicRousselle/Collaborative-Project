using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : RewindableObject
{
    [SerializeField] private bool startDirectionRight = true;

    private bool goingRight = true;

    private void Start()
    {
        goingRight = startDirectionRight;
        SetStateVoid();
    }

    private void Behavior()
    {
        if (goingRight) print("goes right");
        else print("goes left");
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
}
