using System.Collections;
using UnityEngine;

public class Button : InteractableObject
{
    [SerializeField] InteractableObject target = default;

    public override void OnInteract()
    {
        target.OnInteract();
    }
}