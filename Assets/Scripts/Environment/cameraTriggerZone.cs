using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTriggerZone : MonoBehaviour
{
    [SerializeField] private float deZoom;
    [SerializeField] private float baseZoom;
    [SerializeField] private static bool targetting;
    [SerializeField] private bool changeTarget;
    public void Start() 
    {
        baseZoom = PlayerSmoosCamera.offset.z;
        targetting = true;
    }
    public void Update()
    {
        PlayerSmoosCamera.targeting = targetting;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            if (changeTarget)
            {
                PlayerSmoosCamera.offset = new Vector3(PlayerSmoosCamera.offset.x, PlayerSmoosCamera.offset.y, deZoom);
                targetting = false;
            }
            else if (! changeTarget)
            {
                PlayerSmoosCamera.offset = new Vector3(PlayerSmoosCamera.offset.x, PlayerSmoosCamera.offset.y, deZoom);
                
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerSmoosCamera.offset = new Vector3(PlayerSmoosCamera.offset.x, PlayerSmoosCamera.offset.y, baseZoom);
            targetting = true;
        }
    }
}
