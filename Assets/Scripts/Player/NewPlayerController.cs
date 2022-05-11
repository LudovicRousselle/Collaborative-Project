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

    // The timer that we can set in the inspector
    [SerializeField] float jumpBuffering = 0.08f;

    // The current timer for jump buffering
    float currentJumpbuffering;

    [Header("Physics")]
    [SerializeField,Range(1,5)] public float gravityIntensifier = 1.3f;

    [Header("Boxcast")]
    [SerializeField] private Vector3 boxCastSize;
    [SerializeField] Transform boxCastStartingPos;
    [SerializeField] LayerMask groundLayer;

    [Header("Jump")]
    [SerializeField] private string[] jumpableTags;

    public PlayerInput input;

    private Rigidbody rb;

    private Vector2 inputMove = Vector2.zero;

    private bool isGrounded = false;
    private bool wallHit = false;

    private float currentSpeed = 0;
    private float groundSpeed = 0;

    // private RaycastHit hit;

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

        
        // === [Jump Buffering] ===
        if (currentJumpbuffering > 0)
        {currentJumpbuffering -= Time.deltaTime;}
    }

    private void FixedUpdate()
    {
        RayCastGround();
        
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
        else
        {
            currentJumpbuffering = jumpBuffering;
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
        // Check if there is a ground under the player :    Box Center              Dimensions    Direction       angle             Distance            Layer
        RaycastHit[] raycastHits = Physics.BoxCastAll(boxCastStartingPos.position, boxCastSize, Vector3.one, Quaternion.identity, 0, groundLayer);

        if (raycastHits.Length != 0)
        {
            //Debug.Log("Touched something");
            
            foreach (RaycastHit _h in raycastHits)
            {
                //Debug.Log(_h.transform.gameObject.name);

                if (_h.transform.gameObject.CompareTag("Ground") || _h.transform.gameObject.CompareTag("Interactable") || _h.transform.gameObject.CompareTag("Rewindable")) 
                {

                    // Only on the frame we land on the ground 
                    if (!isGrounded)
                    {
                        isGrounded = true;

                        // Jump immediately is buffering activated
                        if (currentJumpbuffering > 0)
                        { 
                            Jump(); 
                        }
                    }

                    wallHit = false;
                
                }
                return;
            }   
        }

        if (isGrounded)
        {
            // Only on the frame we leave the ground
            {
                currentJumpbuffering = 0;
            }
        }

        isGrounded = false;
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
    // private void OnTriggerEnter(Collider collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Interactable"))
    //     {
    //         isGrounded = true;
    //         wallHit = false;
    //         print("grounded");
    //     }
    // }

    private void OnTriggerExit(Collider collision)
    {
        foreach (var item in jumpableTags)
        {
            if (collision.gameObject.CompareTag(item))
            {
                isGrounded = false;
                return;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var item in jumpableTags)
        {
            if (collision.gameObject.CompareTag(item))
            {
                wallHit = true;
                return;
            }
        }
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCastStartingPos.position, boxCastSize * 2);
    }
}