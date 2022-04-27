using System;
using UnityEditor;
using UnityEngine;

enum State
{
    Void,
    Rewind,
    Proceed
}

public class RewindableObject : InteractableObject
{
    private bool isRewinded = false;
    private State state = State.Void;

    override public void OnInteract()
    {
        //if (state != State.Void) return;

        if (!isRewinded)
        {
            state = State.Rewind;
        }
        else
        {
            state = State.Proceed;
        }
    }

    protected void StateMachine()
    {
        switch (state)
        {
            case State.Void:
                break;
            case State.Rewind:
                Debug.Log("Inside State Machine");
                OnRewind();
                break;
            case State.Proceed:
                OnProceed();
                break;
            default:
                break;
        }
    }

    protected virtual void OnRewind()
    {
        Debug.Log("Rewind");
        SetStateVoid();
    }

    protected virtual void OnProceed()
    {
        Debug.Log("Proceed");
        SetStateVoid();
    }

    protected void SetStateVoid()
    {
        state = State.Void;
    }
}