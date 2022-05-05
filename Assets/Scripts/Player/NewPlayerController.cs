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

    [Header("Raycast")]
    [SerializeField] private float downRayLength = 0.5f;

    public PlayerInput input;

    private Rigidbody rb;

    private Vector2 inputMove = Vector2.zero;

    private bool isGrounded = false;
    private bool wallHit = false;

    private float currentSpeed = 0;
    private float groundSpeed = 0;

    private RaycastHit hit;

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

        RayCastGround();
        
    }

    private void FixedUpdate()
    {
        Move();

        if (!isGrounded || wallHit) rb.AddForce(new Vector3(0, -gravityIntensifier * 100, 0));
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
            if (!wallHit) rb.AddForce(new Vector3(inputMove.x * currentSpeed * 100, 0, 0));
        }
    }

    private void RayCastGround()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, downRayLength))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down), Color.blue, downRayLength);
            if (hit.transform.gameObject.CompareTag("Ground") || hit.transform.gameObject.CompareTag("Interactable")) isGrounded = true;
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

    #region Triggers&Collisions
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Interactable"))
        {
            isGrounded = true;
            wallHit = false;
            print("grounded");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Interactable"))
        {
            isGrounded = false;
            print("not grounded");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isGrounded && (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Interactable")))
        {
            wallHit = true;
            print("wallHit");
        }
    }
    #endregion
}