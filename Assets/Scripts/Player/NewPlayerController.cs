using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{
    [Header("Basics")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float speedLimit = 5f;
    [SerializeField] private float jumpImpulse = 5f;

    public PlayerInput input;

    private Rigidbody rb;

    private Vector2 inputMove = Vector2.zero; 
    private Vector2 velocity = Vector2.zero;
    private Vector2 acceleration = Vector2.zero;

    private bool isGrounded = true;

    private float airSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = new PlayerInput();

        SetupAllInputs();
    }

    private void Update()
    {
        //Move();
    }

    private void FixedUpdate()
    {
        //rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -speedLimit, speedLimit),rb.velocity.y);
        rb.velocity = new Vector2(inputMove.x * speed, rb.velocity.y);
    }

    void Move()
    {
        rb.velocity = new Vector2(velocity.x,rb.velocity.y);

        //if (inputMove.x > 0)
        //{
        //    if (isGrounded)
        //        acceleration.x = speed;
        //    else if (!isGrounded)
        //        acceleration.x = airSpeed;
        //}
        //else if (inputMove.x < 0)
        //{
        //    if (isGrounded)
        //        acceleration.x = -speed;
        //    else if (!isGrounded)
        //        acceleration.x = -airSpeed;
        //}
        //else acceleration.x = 0;

        //velocity += acceleration * Time.deltaTime;

        //if(acceleration.x == 0 || (acceleration.x*velocity.x) < 0)
        //{

        //    velocity.x *= (1 - Time.deltaTime * rb.drag);
        //    if (Mathf.Abs(velocity.x) <= 0.5 && inputMove.x == 0)
        //    {
        //        velocity.x = 0;
        //    }
        //}
    }

    private void SetupAllInputs()
    {
        input.Default.Jump.performed += ctx => Jump();
        input.Default.Run.performed += ctx => Run();

        input.Default.Move.performed += ctx => inputMove = ctx.ReadValue<Vector2>();
        input.Default.Move.canceled += ctx => inputMove = Vector2.zero;
    }

    private void Run() { }

    private void Jump() 
    {
        rb.AddForce(new Vector3(0, jumpImpulse*100, 0));
    }

    #region Enable/Disable
    private void OnEnable()
    {
        input?.Enable();
    }

    private void OnDisable()
    {
        input?.Disable();
    }
    #endregion
}