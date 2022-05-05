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
    protected bool isRewinded = false;
    private State state = State.Void;

    override public void OnInteract()//lancé qund le joueur press E à côté
    {
        print(gameObject.name + " is being interacted with");
        if (state != State.Void) return; //si l'objet n'est pas en state void la fonction s'arrête ici

        print("yeah");
        if (!isRewinded)
            state = State.Rewind; //si il est pas rewinded, il rewind
        else 
            state = State.Proceed;//sinon il proceed
    }

    private void Update()
    {
        StateMachine();
    }

    private void StateMachine() // appelle les fonctions de states en fonction du state actif
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

    protected virtual void OnRewind()// se joue quand le state est rewind
    {
        Debug.Log("Rewinded");
        isRewinded = true;
        SetStateVoid();
    }

    protected virtual void OnProceed()// se joue quand le state est proceed
    {
        Debug.Log("Proceeded");
        isRewinded = false;
        SetStateVoid();
    }

    protected virtual void OnVoid() { } // se joue quand le state est void

    protected void SetStateVoid()
    {
        state = State.Void;
    }

    protected void SetStateRewind()
    {
        state = State.Rewind;
    }

    protected void SetStateProceed()
    {
        state = State.Proceed;
    }
}