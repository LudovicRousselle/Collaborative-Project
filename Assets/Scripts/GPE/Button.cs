using System.Collections;
using UnityEngine;

public class Button : InteractableObject
{
    [SerializeField] RewindableObject target = default;

    public override void OnInteract()
    {
        target.OnInteract();
    }
}