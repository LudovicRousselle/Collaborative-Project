using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInput input;

    [Header("RunSpeed")]
    [SerializeField] private float walkingSpeed = 0.5f;
    [SerializeField] private float maxSpeedDivider = 10f; 
    [SerializeField] private float airSpeed = 0.1f;
    [SerializeField] private float runMultiplier = 2;

    [Header("Jump")]
    [SerializeField] private float jumpImpulse = 5;

    [Header("Fall")]
    [SerializeField] private float fallSpeed = 0.1f;
    [SerializeField] private float maxFallSpeed = 0.2f;
    
    [Header("Physics")]
    [SerializeField] private float groundFriction = 0.2f;
    [SerializeField] private float airFriction = 0.1f;

    [Header("RayCast Rays")]
    [SerializeField] private float downRayDistance = 2;
    [SerializeField] private float sideRayDistance = 1.1f;
    [SerializeField] private float distToReplacePlayer = 0.1f;

    [Space(20)]
    [SerializeField] private string groundTag = "Ground";

    private float maxWalkingSpeed = 5f;
    private float speed = 1;
    private float maxSpeed = 1;
    private float runningSpeed = 1;
    private float maxRunningSpeed = 1;

    private Vector2 velocity = Vector2.zero;
    private Vector2 acceleration = Vector2.zero;
    private Vector2 inputMove = Vector2.zero;

    private bool isGrounded = false;
    private bool canJumpBuffer = false;

    private void Awake()
    {
        input = new PlayerInput();

        maxWalkingSpeed = walkingSpeed / maxSpeedDivider;

        runningSpeed = walkingSpeed * runMultiplier;
        maxRunningSpeed = maxWalkingSpeed * runMultiplier;

        speed = walkingSpeed;
        maxSpeed = maxWalkingSpeed;

        SetupAllInputs();
    }

    #region Updates
    void Update()
    {
        Move();

        print(velocity);
    }

    void FixedUpdate()
    {
        SimulatePhysics();
    }
    #endregion

    private void SetupAllInputs()
    {
        input.Default.Jump.performed += ctx => Jump();
        input.Default.Run.performed += ctx => Run();

        input.Default.Move.performed += ctx => inputMove = ctx.ReadValue<Vector2>();
        input.Default.Move.canceled += ctx => inputMove = Vector2.zero;
    }

    private void Jump()
    {
        if (isGrounded || canJumpBuffer)
        {
            isGrounded = false;
            canJumpBuffer = false;
            acceleration.y = jumpImpulse;
            velocity.y = 0;
        }
    }

    private void Run()
    {
        print("run");
        if (speed == walkingSpeed)
        {
            speed = runningSpeed;
            maxSpeed = maxRunningSpeed;
        } 
        else if (speed == runningSpeed)
        {
            speed = walkingSpeed;
            maxSpeed = maxWalkingSpeed;
        }
    }

    void Move()
    {
        transform.position += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime; //adds the velocity to the player every frame

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

            if (Mathf.Abs(velocity.x) <= 0.5 && inputMove.x == 0)
            {
                velocity.x = 0;
            }

            if (acceleration.x == 0) velocity.x /= 1.0001f;
        }

        if (RaycastTest())
        {
            if (velocity.y < 0)
                canJumpBuffer = true;
            else 
                canJumpBuffer = false;
        }
        else isGrounded = false;

        SideRayCast();
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

    private bool RaycastTest()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * downRayDistance, Color.red);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, downRayDistance))
        {
            if (hit.transform.gameObject.CompareTag(groundTag))
            {
                return true;
            }
        }
        return false;
    }

    private void SideRayCast()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * sideRayDistance, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * sideRayDistance, Color.red);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, sideRayDistance))
        {
            if (hit.transform.gameObject.CompareTag(groundTag))
            {
                velocity.x = 0;
                transform.position = new Vector3(transform.position.x - distToReplacePlayer, transform.position.y, transform.position.z);
            }
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, sideRayDistance))
        {
            if (hit.transform.gameObject.CompareTag(groundTag))
            {
                velocity.x = 0;
                transform.position = new Vector3(transform.position.x + distToReplacePlayer, transform.position.y, transform.position.z);
            }
        }
    }
    
}