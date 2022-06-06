using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.OnDeath();
        }
        else if (collision.gameObject.CompareTag("Robots"))
        {
            Turret turret = collision.GetComponentInChildren<Turret>();
            turret.DeathVFX();
            Destroy(collision.gameObject);
        }
    }
}
