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
    [SerializeField] public float gravityIntensifier = 1.3f;

    [Header("Boxcast")]
    [SerializeField] private Vector3 boxCastSize;
    [SerializeField] Transform boxCastStartingPos;
    [SerializeField] LayerMask groundLayer;

    [Header("Jump")]
    [SerializeField] private string[] jumpableTags;

    [Header("Wall Detection")]
    [SerializeField] private float sideRayDist = 0.5f;
    private float additionalSideRayDist;
    private int wallHitDir;

    public PlayerInput input;

    //Animations
    private Animator m_animator;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isMoving = false;
    private float delayJump = 0;
    private float delayMove = 0;
    private float delayIdle = 0;
    private bool isRewinding = false;
    private float delayRewind = 0;


    private Rigidbody rb;

    private Vector2 inputMove = Vector2.zero;
    private Vector3 currentScale;


    private float currentSpeed = 0;
    private float groundSpeed = 0;

    // private RaycastHit hit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        m_animator = GetComponentInChildren<Animator>();
        m_animator.enabled = true;
        input = new PlayerInput();

        SetupAllInputs();
    }

    private void Start()
    {
        groundSpeed = walkSpeed;
        currentSpeed = groundSpeed;

        currentScale = transform.localScale;

        additionalSideRayDist = GetComponent<CapsuleCollider>().radius * 0.8f;
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

        if (isJumping)
        {
            delayJump += Time.deltaTime;
        }
        else
        {
            delayMove += Time.deltaTime;
            delayJump = 0;
        }

        if (isGrounded && isJumping && delayJump >= 0.5f)
        {
            if (!isRewinding)
            {
                m_animator.Play("Anim_JumpEnd", 0);
            }
            m_animator.Play("Anim_JumpEnd", 1);
            delayMove = 0;
            isJumping = false;
        }

        if (!isGrounded || isJumping)
        {
            isMoving = false;
        }

        if (!isGrounded && !isJumping)
        {
            isJumping = true;
            m_animator.Play("Anim_JumpDuration", 0);
            m_animator.Play("Anim_JumpDuration", 1);
        }

        if (isRewinding)
        {
            delayRewind += Time.deltaTime;

            if (delayRewind >= 1.5f)
            {
                isRewinding = false;
            }
        }else
        {
            delayRewind = 0;
        }

        if (inputMove.x == 0)
        {
            delayIdle += Time.deltaTime;

            if (!isJumping && delayMove >= 0.75f && delayIdle >= 0.1f)
            {
                m_animator.Play("Anim_Idle", 1);

                if (!isRewinding)
                {
                    m_animator.Play("Anim_Idle", 0);
                }
                delayIdle = 0;
                isMoving = false;
            }
        }

    }

    private void FixedUpdate()
    {
        RayCastGround();
        RayCastWalls();

        Move();

        if (!isGrounded) rb.AddForce(new Vector3(0, -gravityIntensifier * 100, 0));
    }

    private void Run() 
    {
        //if (groundSpeed == runSpeed) 
        //{
        //    m_moveAnim = "Anim_Walk";
        //    groundSpeed = walkSpeed;
        //}
        //else
        //{
        //    m_moveAnim = "Anim_Run";
        //    groundSpeed = runSpeed;
        //}
    }

    private void Jump() 
    {   
        if (isGrounded)
        {
            isJumping = true;
            isMoving = false;

            m_animator.Play("Anim_JumpStart", 1);

            if (!isRewinding)
            {
                m_animator.Play("Anim_JumpStart", 0);
            }

            currentSpeed = airSpeed;
            rb.AddForce(new Vector3(0, jumpImpulse * 1000, 0));
        }
        else
        {
            currentJumpbuffering = jumpBuffering;
        }
    }

    private Vector3 activeScale = Vector3.zero;

    private void Move()
    {
        if (wallHitDir > 0) inputMove.x = Mathf.Clamp(inputMove.x, -1000, 0);
        else if (wallHitDir < 0) inputMove.x = Mathf.Clamp(inputMove.x, 0, 1000);

        if (inputMove.x > 0 || inputMove.x < 0)
        {
            rb.AddForce(new Vector3(inputMove.x * currentSpeed * 100, 0, 0));

            //if (transform.parent != null)
            //{
            //     activeScale = new Vector3(
            //        currentScale.x * inputMove.x / transform.parent.localScale.x, 
            //        currentScale.y / transform.parent.localScale.y, 
            //        currentScale.z / transform.parent.localScale.z);

            //    if (transform.parent.parent != null)
            //    {
            //        activeScale = new Vector3 (
            //            activeScale.x / transform.parent.parent.localScale.x, 
            //            activeScale.y / transform.parent.parent.localScale.y,
            //            activeScale.z / transform.parent.parent.localScale.z);

            //        if (transform.parent.parent.parent != null)
            //        {
            //            activeScale = new Vector3(
            //                activeScale.x / transform.parent.parent.parent.localScale.x,
            //                activeScale.y / transform.parent.parent.parent.localScale.y,
            //                activeScale.z / transform.parent.parent.parent.localScale.z);
            //        }
            //    }

            //}
            //else 
            //    activeScale = new Vector3(currentScale.x * inputMove.x, currentScale.y, currentScale.z);

            if (transform.parent != null && transform.parent.parent != null && transform.parent.parent.parent != null)
            {
                transform.localScale = new Vector3(
                            currentScale.x * inputMove.x / transform.parent.localScale.x / transform.parent.parent.localScale.x / transform.parent.parent.parent.localScale.x,
                            currentScale.y / transform.parent.localScale.y / transform.parent.parent.localScale.y / transform.parent.parent.parent.localScale.y,
                            currentScale.z / transform.parent.localScale.z / transform.parent.parent.localScale.z / transform.parent.parent.parent.localScale.z);
            }
            else if (transform.parent != null && transform.parent.parent != null)
            {
                transform.localScale = new Vector3(
                            currentScale.x * inputMove.x / transform.parent.localScale.x / transform.parent.parent.localScale.x,
                            currentScale.y / transform.parent.localScale.y / transform.parent.parent.localScale.y,
                            currentScale.z / transform.parent.localScale.z / transform.parent.parent.localScale.z);
            }
            else if (transform.parent != null)
            {
                transform.localScale = new Vector3(
                            currentScale.x * inputMove.x / transform.parent.localScale.x,
                            currentScale.y / transform.parent.localScale.y,
                            currentScale.z / transform.parent.localScale.z);
            }
            else
                transform.localScale = new Vector3(currentScale.x * inputMove.x, currentScale.y, currentScale.z);

            if (isGrounded && !isMoving && !isJumping && delayMove >= 0.25f)
            {
                if (!isRewinding)
                {
                    m_animator.Play("Anim_Walk", 0);
                }
                m_animator.Play("Anim_Walk", 1);
                isMoving = true;
            }
        }
    }

    public void RewindAnimation()
    {
        isRewinding = true;
        m_animator.Play("Anim_RewindStart", 0);
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

                return;
            }   
        }else
        {
            isGrounded = false;
        }

        if (isGrounded)
        {
            // Only on the frame we leave the ground
            {
                currentJumpbuffering = 0;
            }
        }

        //isGrounded = false;
    }

    private void RayCastWalls()
    {
        RaycastHit hit = new RaycastHit();
        float rayDist = additionalSideRayDist + sideRayDist;

        if (Physics.Raycast(transform.position, Vector3.right, out hit, rayDist))
        {
            if (hit.collider.isTrigger == true) return;

            wallHitDir = 1;
            Debug.DrawRay(transform.position, Vector3.right * rayDist, Color.yellow);
        }
        else if (Physics.Raycast(transform.position, -Vector3.right, out hit, rayDist))
        {
            if (hit.collider.isTrigger == true) return;

            wallHitDir = -1;
            Debug.DrawRay(transform.position, -Vector3.right * rayDist, Color.yellow);
        }
        else
        {
            wallHitDir = 0;
            Debug.DrawRay(transform.position, Vector3.right * rayDist, Color.white);
            Debug.DrawRay(transform.position, -Vector3.right * rayDist, Color.white);
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
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCastStartingPos.position, boxCastSize * 2);
    }
}