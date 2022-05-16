using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTriggerZone : MonoBehaviour
{
    [SerializeField] private float deZoom;
    [SerializeField] private float baseZoom;
    public void Start()
    {
        baseZoom = PlayerSmoosCamera.offset.z;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            
            PlayerSmoosCamera.offset=new Vector3(PlayerSmoosCamera.offset.x, PlayerSmoosCamera.offset.y, deZoom);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerSmoosCamera.offset = new Vector3(PlayerSmoosCamera.offset.x, PlayerSmoosCamera.offset.y, baseZoom);
        }
    }
}
