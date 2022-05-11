using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFallingBlock : RewindableObject
{
    [SerializeField] private float fallingSpeed;
    [SerializeField] public string targetTag = "Player";
    [SerializeField] public string groundTag = "Ground";

    //public GameObject player;

    private FallingBlockTriggerZone triggerZone;
    private Killzone killzone;
    private Rigidbody rb;
    private bool triggered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        triggerZone = GetComponentInChildren<FallingBlockTriggerZone>();

        if (triggerZone == null)
        {
            Debug.LogError("The falling block object needs a trigger hitbox (place it as a child with a trigger hitbox and the FallingBoxTriggerZone.cs)");
        }

        killzone = GetComponentInChildren<Killzone>();
    }

    protected override void DoAction()
    {
        rb.AddForce(-Vector3.up * fallingSpeed);

        if(!killzone.gameObject.activeSelf) killzone.gameObject.SetActive(true);
    }

    protected override void OnVoid()
    {
        if (triggerZone.triggered || triggered)
        {
            SetStateAction();
            triggered = true;
        }

        base.OnVoid();
    }

    protected override void DuringRewind()
    {
        rb.AddForce(Vector3.up * fallingSpeed);

        //if (player != null)
        //{
        //    player.GetComponent<Rigidbody>().AddForce(Vector3.up * (fallingSpeed + (player.GetComponent<NewPlayerController>().gravityIntensifier * 100)));
        //}
    }

    protected override void EndRewind()
    {
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //    collision.gameObject.transform.parent = transform;
        //    player = collision.gameObject;
        //}
        
        if (collision.gameObject.CompareTag(groundTag))
        {
            killzone.gameObject.SetActive(false);
            print("le sol je le touche en tant que block qui fall");
            SetStateVoid();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
            //player = null;
        }
    }
}
