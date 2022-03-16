using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float fallSpeed = 0.1f;
    [SerializeField] float maxFallSpeed = 0.2f;

    [SerializeField] string groundTag = "Ground";

    private Vector2 velocity = Vector2.zero;
    private Vector2 acceleration = Vector2.zero;

    private bool isGrounded = false;
    void Update()
    {
        Move();
    }

    void FixedUpdate()
    {
        SimulatePhysics();
    }

    void Move()
    {
        transform.position += new Vector3(velocity.x, velocity.y, 0); //adds the velocity to the player every frame
    }

    void SimulatePhysics()
    {
        velocity += acceleration;
        velocity.y = Mathf.Clamp(velocity.y,maxFallSpeed,10);

        if (!isGrounded)
        {
            acceleration.y = -fallSpeed * Time.deltaTime;
        }
        else
        {
            acceleration.y = 0;
            velocity.y = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision);
        if (collision.gameObject.CompareTag(groundTag))
        {
            isGrounded = true;
        }
    }
}
