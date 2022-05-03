using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{
    [Header("Basics")]
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float runSpeed = 3f;
    [SerializeField] private float airSpeed = 3f;
    [SerializeField] private float jumpImpulse = 5f;

    [Header("Physics")]
    [SerializeField,Range(1,5)] private float gravityIntensifier = 1.3f; 

    public PlayerInput input;

    private Rigidbody rb;

    private Vector2 inputMove = Vector2.zero;

    private bool isGrounded = false;

    private float currentSpeed = 0;
    private float groundSpeed = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input = new PlayerInput();

        SetupAllInputs();
    }

    private void Start()
    {
        groundSpeed = walkSpeed;
        currentSpeed = groundSpeed;
    }

    private void SetupAllInputs()
    {
        input.Default.Jump.performed += ctx => Jump();
        input.Default.Run.performed += ctx => Run();

        input.Default.Move.performed += ctx => inputMove = ctx.ReadValue<Vector2>();
        input.Default.Move.canceled += ctx => inputMove = Vector2.zero;
    }

    private void Update()
    {
        if (!isGrounded) currentSpeed = airSpeed;
        else if (isGrounded) currentSpeed = groundSpeed;
    }

    private void FixedUpdate()
    {
        Move();

        if (!isGrounded) rb.AddForce(new Vector3(0, -gravityIntensifier*100, 0));
    }

    private void Run() 
    {
        if (groundSpeed == runSpeed) groundSpeed = walkSpeed;
        else groundSpeed = runSpeed;
    }

    private void Jump() 
    {
        if (isGrounded)
        {
            currentSpeed = airSpeed;
            rb.AddForce(new Vector3(0, jumpImpulse * 1000, 0));
        }
    }

    private void Move()
    {
        if (inputMove.x > 0 || inputMove.x < 0)
        {
            rb.AddForce(new Vector3(inputMove.x * currentSpeed * 100, 0, 0));
        }
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

    #region triggers
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            print("grounded");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            print("not grounded");
        }
    }
    #endregion
}