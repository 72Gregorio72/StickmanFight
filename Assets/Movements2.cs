using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    private float horizontalMovement;
    private float moveDirection;

    [Header("Jump")]
    public float jumpForce = 20f;
    public int maxJumps = 2;
    private int jumpCount;

    [Header("Wall Slide")]
    public float wallSlideSpeed = 2f;

    [Header("Wall Jump")]
    public Vector2 wallJumpForce = new Vector2(15f, 20f);
    public float wallJumpDuration = 0.2f;

    [Header("Gravity")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float groundingForce = -1f;
    public float jumpEndEarlyGravityModifier = 2f;
    public float maxFallSpeed = -20f;
    private bool endedJumpEarly = false;

    private Rigidbody2D rb;
    private bool isFacingRight = true;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Wall Detection")]
    public Vector2 wallCheckOffset = new Vector2(0.5f, 0f);
    public float wallCheckRadius = 0.1f;
    public LayerMask wallLayer;
    private bool isTouchingWallLeft;
    private bool isTouchingWallRight;
    private bool isWallSliding;
    private bool isWallJumping;

    private bool isHoldingJump;

    [Header("Animation")]
    public Animator anim;
    public float speed;
    public bool jumping;
    public bool wallSliding;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        moveDirection = horizontalMovement * moveSpeed;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        Vector2 wallCheckLeftPos = (Vector2)transform.position - new Vector2(wallCheckOffset.x, wallCheckOffset.y);
        Vector2 wallCheckRightPos = (Vector2)transform.position + new Vector2(wallCheckOffset.x, wallCheckOffset.y);

        isTouchingWallLeft = Physics2D.OverlapCircle(wallCheckLeftPos, wallCheckRadius, wallLayer);
        isTouchingWallRight = Physics2D.OverlapCircle(wallCheckRightPos, wallCheckRadius, wallLayer);

        if (isGrounded)
        {
            jumpCount = 0;
            anim.SetBool("jumping", false);
            anim.SetBool("wallSliding", false);
        }

        if(Input.GetButtonDown("Jump")){
            isHoldingJump = true;
        }

        if(Input.GetButtonUp("Jump")){
            isHoldingJump = false;
        }

        if (!wallSliding)
        {
            anim.SetBool("wallSliding", false);
            if (!isGrounded)
            {
                anim.SetBool("jumping", true);
            }
        }

        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount < maxJumps))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetBool("jumping", true);
            anim.SetBool("wallSliding", false);
            jumpCount++;
            endedJumpEarly = false;
        }

        if ((isTouchingWallLeft || isTouchingWallRight) && !isGrounded && !isWallJumping && rb.velocity.y < 0 && !Input.GetButton("Jump"))
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if(isHoldingJump){
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
            anim.SetBool("wallSliding", true);
            anim.SetBool("jumping", false);
        }

        Debug.Log(isHoldingJump);

        if ((isTouchingWallLeft || isTouchingWallRight) && Input.GetButton("Jump") && Mathf.Abs(horizontalMovement) > 0)
        {
            anim.SetBool("jumping", true);
            anim.SetBool("wallSliding", false);
            if (isTouchingWallLeft)
            {
                rb.velocity = new Vector2(wallJumpForce.x, wallJumpForce.y);
            }
            else if (isTouchingWallRight)
            {
                rb.velocity = new Vector2(-wallJumpForce.x, wallJumpForce.y);
            }
            endedJumpEarly = false;
            isWallJumping = true;
            Invoke("ResetWallJump", wallJumpDuration);
        }

        ApplyGravity();
        Flip(horizontalMovement);

        anim.SetFloat("speed", Mathf.Abs(horizontalMovement));
    }

    void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(moveDirection, rb.velocity.y);
        }
    }

    private void ApplyGravity()
    {
        if (isGrounded && rb.velocity.y <= 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, groundingForce);
        }
        else if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed));
        }
        else
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                endedJumpEarly = true;
                rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpEndEarlyGravityModifier - 1) * Time.deltaTime;
            }

            if (rb.velocity.y < maxFallSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
            }
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !isFacingRight || horizontal < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }

    private void ResetWallJump()
    {
        isWallJumping = false;
    }
}
