using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput input;

    [Header("RunSpeed")]
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float speed = 0.5f;
    [SerializeField] float airSpeed = 0.1f;

    [Header("Jump")]
    [SerializeField] float jumpImpulse = 5;

    [Header("Fall")]
    [SerializeField] float fallSpeed = 0.1f;
    [SerializeField] float maxFallSpeed = 0.2f;

    [Header("Physics")]
    [SerializeField] float groundFriction = 0.2f;
    [SerializeField] float airFriction = 0.1f;

    [SerializeField] string groundTag = "Ground";

    private Vector2 velocity = Vector2.zero;
    private Vector2 acceleration = Vector2.zero;
    private Vector2 inputMove = Vector2.zero;

    private bool isGrounded = false;

    private void Awake()
    {
        input = new PlayerInput();
        maxSpeed /= 100;

        SetupAllInputs();
    }

    void Update()
    {
        Move();
    }

    void FixedUpdate()
    {
        SimulatePhysics();
    }

    private void SetupAllInputs()
    {
        input.Default.Jump.performed += ctx => Jump();

        input.Default.Move.performed += ctx => inputMove = ctx.ReadValue<Vector2>();
        input.Default.Move.canceled += ctx => inputMove = Vector2.zero;
    }

    private void Jump()
    {
        if (!isGrounded) return;
        isGrounded = false;
        acceleration.y = jumpImpulse;
    }

    void Move()
    {
        transform.position += new Vector3(velocity.x, velocity.y, 0); //adds the velocity to the player every frame

        if (inputMove.x > 0)
        {
            if (isGrounded)
                acceleration.x = speed;
            else if(!isGrounded)
                acceleration.x = airSpeed;
        }
        else if (inputMove.x < 0)
        {
            if (isGrounded)
                acceleration.x = -speed;
            else if (!isGrounded)
                acceleration.x = -airSpeed;
        }
        else acceleration.x = 0;
    }

    void SimulatePhysics()
    {
        velocity += acceleration * Time.deltaTime;
        velocity.y = Mathf.Clamp(velocity.y,-maxFallSpeed,100);
        velocity.x = Mathf.Clamp(velocity.x,-maxSpeed,maxSpeed);

        if (!isGrounded)
        {
            acceleration.y = -fallSpeed;
        }

        if (velocity.x != 0)
        {
            float velocitySymbol = (velocity.x / Mathf.Abs(velocity.x));

            if (!isGrounded) 
            {
                velocity.x -= velocitySymbol * airFriction * Time.deltaTime;
            }
            else
            {
                velocity.x -= velocitySymbol * groundFriction * Time.deltaTime;
            }

            if (Mathf.Abs(velocity.x) <= 0.005 && inputMove.x == 0)
            {
                velocity.x = 0;
            }
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            acceleration.y = 0;
            velocity.y = 0;
            isGrounded = true;
        }
    }

    private void OnEnable()
    {
        input?.Enable();
    }

    private void OnDisable()
    {
        input?.Disable();
    }
}