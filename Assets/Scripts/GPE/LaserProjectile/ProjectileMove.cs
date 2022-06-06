using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    private Transform m_player;

    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;

        if (muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;

            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy (muzzleVFX, psMuzzle.main.duration);
            }else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }

        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_player.position, speed * Time.deltaTime);
        }else
        {
            Debug.Log("No speed");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (hitPrefab != null)
            {
                var hitVFX = Instantiate(hitPrefab, transform.position, Quaternion.identity);

                var psHit = hitVFX.GetComponent<ParticleSystem>();
                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }

            other.SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);

            speed = 0;
            Destroy(gameObject);
        }
    }
}
