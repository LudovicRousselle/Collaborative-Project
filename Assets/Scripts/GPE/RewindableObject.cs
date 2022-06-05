using System;
using UnityEditor;
using UnityEngine;

enum State
{
    Void,
    Rewind,
    Action
}

public class RewindableObject : InteractableObject
{
    [SerializeField] protected float rewindedStateDuration;
    [SerializeField] protected Outline outlineScript;

    private bool isRewinding = false;
    public bool IsRewinding
    {
        get { return isRewinding; }
    }

    private void Awake()
    {
        gameObject.tag = "Rewindable";
        
    }

    private State state = State.Void;
    protected float counter = 0;

    override public void OnInteract()//lancé qund le joueur press E à côté
    {
        print(gameObject.name + " is being interacted with");

        if (!isRewinding)
        {
            SetStateRewind(); //si il est pas entrain de rewind, il rewind
        }
        else
        {
            SetStateAction(); //sinon il proceed
        }
    }

    private void Update()
    {
        StateMachine();
    }

    protected void StateMachine() // appelle les fonctions de states en fonction du state actif
    {
        switch (state)
        {
            case State.Void:
                OnVoid();
                break;
            case State.Rewind:
                DoRewind();
                break;
            case State.Action:
                DoAction();
                break;
            default:
                break;
        }
    }

    private void DoRewind() // se joue quand le state est rewind
    {
        /* Un compteur (counter) s'incrémente de Time.deltatime par frame
         * pendant qu'il monte, DuringRewind() se joue
         * lorsqu'il dépasse rewindedStateDuration il reset le counter
         * le booléen isRewinding se met a false
         * la fonction EndRewind se joue
         * le state passe de rewind à action
         */

        if ((counter += Time.deltaTime) > rewindedStateDuration)
        {
            counter = 0;
            isRewinding = false;
            EndRewind();
            SetStateAction();
        }

        DuringRewind();
    }

    protected virtual void DoAction() { } // se joue quand le state est action

    protected virtual void OnVoid() { } // se joue quand le state est void

    protected virtual void DuringRewind() { } // se joue quand le rewind est en cours
    protected virtual void EndRewind() { } // se joue quand le rewind se termine

    public virtual void InterruptRewind() 
    {
        SetStateAction();
    }

    #region SetState
    protected virtual void SetStateVoid()
    {
        state = State.Void;

        if (outlineScript == null)
        {
            Debug.LogError("No Outline was detected on item " + gameObject.name);
        }
        else
        {
            outlineScript.enabled = true;
        }
    }

    protected virtual void SetStateRewind()
    {
        state = State.Rewind;
        isRewinding = true;
        counter = 0;
        if (outlineScript == null)
        {
            Debug.LogError("No Outline was detected on item " + gameObject.name);
        }
        else
        {
            outlineScript.enabled = false;
        }
    }

    protected virtual void SetStateAction()
    {
        state = State.Action;
        isRewinding = false;
        counter = 0;
        if (outlineScript == null)
        {
            Debug.LogError("No Outline was detected on item " + gameObject.name);
        }
        else
        {
            outlineScript.enabled = true;
        }
    }
    #endregion
}