using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindableGear : RewindableObject
{
    [SerializeField] private GameObject parentFolder;

    protected override void DoAction()
    {
    }

    protected override void OnVoid()
    {
        if (parentFolder != null)
        {
            SetStateAction();
        }

        base.OnVoid();
    }

    protected override void DuringRewind()
    {
        Turret turret = parentFolder.GetComponentInChildren<Turret>();
        turret.RobotDeath();
        gameObject.tag = "Default";
    }

    protected override void EndRewind()
    {

    }


    protected override void SetStateRewind()
    {
        base.SetStateRewind();
    }
}
