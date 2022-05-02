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
        print(gameObject.name + " is being interacted with");
        if (state != State.Void) return;

        print("yeah");
        if (!isRewinded)
            state = State.Rewind;
        else 
            state = State.Proceed;
    }

    private void Update()
    {
        StateMachine();
    }

    protected void StateMachine()
    {
        switch (state)
        {
            case State.Void:
                OnVoid();
                break;
            case State.Rewind:
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
        Debug.Log("Rewinded");
        isRewinded = true;
        SetStateVoid();
    }

    protected virtual void OnProceed()
    {
        Debug.Log("Proceeded");
        isRewinded = false;
        SetStateVoid();
    }

    protected virtual void OnVoid() { }

    protected void SetStateVoid()
    {
        state = State.Void;
    }
}