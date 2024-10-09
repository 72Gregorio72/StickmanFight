using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float horizontalMovement;
    private float currentHorizontalSpeed;

    [Header("Jump")]
    public float jumpForce = 20f;
    public int maxJumps = 2;
    private int jumpCount;

    [Header("Wall Slide")]
    public float wallSlideSpeed = 2f;

    [Header("Wall Jump")]
    public Vector2 wallJumpForce = new Vector2(15f, 20f);

    [Header("Gravity")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float groundingForce = -1f;
    public float jumpEndEarlyGravityModifier = 2f;
    public float maxFallSpeed = -20f;
    private bool endedJumpEarly = false;

    public Rigidbody2D rb;
    private bool isFacingRight = true;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Coyote Time")]
    public float coyoteTime = 0.2f; // Duration of coyote time
    private float coyoteTimeCounter; // Timer for coyote time

    [Header("Wall Detection")]
    public Vector2 wallCheckOffset = new Vector2(0.5f, 0f);
    public float wallCheckRadius = 0.1f;
    public LayerMask wallLayer;
    private bool isTouchingWallLeft;
    private bool isTouchingWallRight;
    private bool isWallSliding;
    private bool isWallJumping;

    [Header("Animation")]
    public Animator anim;
    public float speed;
    public bool jumping;
    public bool wallSliding;

    Sounds sounds;

    Vector2 wallCheckLeftPos1;

    Vector2 wallCheckRightPos1;

    public Vector2 boxSize = new Vector2(1f, 1f); // Size of the box
    public Vector2 boxOffset = new Vector2(4f, 0.5f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        sounds = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Sounds>();
    }

    private float jumpCooldown = 0.5f;
    private float jumpTimer = 0f;

    void Update()
    {
        
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed;

        if(horizontalMovement != 0){
            currentHorizontalSpeed = horizontalMovement;
        }

        if (horizontalMovement == 0 && !isGrounded)
        {
            horizontalMovement = currentHorizontalSpeed;
        }

        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        Vector2 wallCheckLeftPos = (Vector2)transform.position - new Vector2(wallCheckOffset.x, -wallCheckOffset.y);
        Vector2 wallCheckRightPos = (Vector2)transform.position + new Vector2(wallCheckOffset.x, wallCheckOffset.y);

        wallCheckLeftPos1 = wallCheckLeftPos;
        wallCheckRightPos1 = wallCheckRightPos;

        isTouchingWallLeft = Physics2D.OverlapCircle(wallCheckLeftPos, wallCheckRadius, wallLayer);
        isTouchingWallRight = Physics2D.OverlapCircle(wallCheckRightPos, wallCheckRadius, wallLayer);

        if (isGrounded)
        {
            jumpCount = 0;
            anim.SetBool("jumping", false);
            anim.SetBool("wallSliding", false);
            coyoteTimeCounter = coyoteTime; // Reset coyote time counter
            moveSpeed = 10f;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // Decrease coyote time counter
            moveSpeed = 11f;
        }  

        if (!isWallSliding)
        {
            anim.SetBool("wallSliding", false);
            if (!isGrounded)
            {
                anim.SetBool("jumping", true);
            }
        }

        if (Input.GetButton("Jump") && (isGrounded && (jumpCount < maxJumps ) || coyoteTimeCounter > 0f ) && jumpTimer <= 0.1f)
        {
            if(!isTouchingWallLeft || !isTouchingWallRight){
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                anim.SetBool("jumping", true);
                anim.SetBool("wallSliding", false);
                sounds.PlayJump();
                sounds.StopWalk();
                sounds.StopSlide();
                jumpCount++;
                endedJumpEarly = false;
                coyoteTimeCounter = 0f; // Reset coyote time counter to avoid multiple jumps
                jumpTimer = jumpCooldown; // Set jump timer
            }
        }

        if(Input.GetButtonDown("Fire1")){
            Attack();
        }

        if (jumpTimer > 0f)
        {
            jumpTimer -= Time.deltaTime; // Decrease jump timer
        }

        if ((isTouchingWallLeft || isTouchingWallRight) && !isGrounded && !isWallJumping)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            anim.SetBool("wallSliding", true);
            anim.SetBool("jumping", false);

            sounds.PlaySlide();

            if (!Input.GetButton("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, 0, float.MaxValue));
            }
        }

        if ((isTouchingWallLeft || isTouchingWallRight) && Input.GetButton("Jump"))
        {
            if (isTouchingWallLeft && currentHorizontalSpeed > 0)
            {
                // Wall jump to the right
                anim.SetBool("jumping", true);
                anim.SetBool("wallSliding", false);
                rb.velocity = new Vector2(wallJumpForce.x, wallJumpForce.y);
                endedJumpEarly = false;
                isWallJumping = true;
                coyoteTimeCounter = 0f;
                Invoke("ResetWallJump", 0.2f);
                sounds.PlayJump();
                sounds.StopWalk();
                sounds.StopSlide();
            }
            else if (isTouchingWallRight && currentHorizontalSpeed < 0)
            {
                // Wall jump to the left
                anim.SetBool("jumping", true);
                anim.SetBool("wallSliding", false);
                rb.velocity = new Vector2(-wallJumpForce.x, wallJumpForce.y);
                endedJumpEarly = false;
                isWallJumping = true;
                coyoteTimeCounter = 0f;
                Invoke("ResetWallJump", 0.2f);
                sounds.PlayJump();
                sounds.StopWalk();
                sounds.StopSlide();
            }
        }

        ApplyGravity();

        Flip(currentHorizontalSpeed);

        // Set animation values
        anim.SetFloat("speed", Mathf.Abs(currentHorizontalSpeed));

        if (Mathf.Abs(currentHorizontalSpeed) > 0.1f && isGrounded)
        {
            sounds.PlayWalk();
        }
        else
        {
            sounds.StopWalk();
        }

        if (!isWallSliding || (isWallSliding && (currentHorizontalSpeed > 0.1f || currentHorizontalSpeed < -0.1f)))
        {
            sounds.StopSlide();
        }
    }

    void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontalMovement, rb.velocity.y); // Use current horizontal speed instead of horizontalMovement
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
            if (isTouchingWallLeft && currentHorizontalSpeed < 0.1f)
            {
                currentHorizontalSpeed = 0;
            }
            else if (isTouchingWallRight && currentHorizontalSpeed > -0.1f)
            {
                currentHorizontalSpeed = 0;
            }
            if (!Input.GetButton("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed));
            }
            else
            {
                if (isTouchingWallLeft)
                {
                    horizontalMovement = -2;
                    sounds.StopSlide();
                }
                else
                {
                    horizontalMovement = 2;
                    sounds.StopSlide();
                }
            }
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

    public void Jump(float jumpForce1)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce1);
        anim.SetBool("jumping", true);
        anim.SetBool("wallSliding", false);
        sounds.PlayJump();
        sounds.StopWalk();
        sounds.StopSlide();
        endedJumpEarly = false;
        coyoteTimeCounter = 0f; // Reset coyote time counter to avoid multiple jumps
    }

    private void Attack(){
        Vector2 boxCenter = new Vector2(0.1f, 0.1f);
        if(isFacingRight){
            boxCenter = (Vector2)transform.position + new Vector2(boxOffset.x, boxOffset.y); // Center of the box
        } else {
            boxCenter = (Vector2)transform.position + new Vector2(-boxOffset.x, boxOffset.y); // Center of the box
        }

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(10);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(wallCheckLeftPos1, wallCheckRadius);
        Gizmos.DrawWireSphere(wallCheckRightPos1, wallCheckRadius);

        Gizmos.color = Color.yellow;
        Vector2 boxCenter = new Vector2(0.1f, 0.1f);
        if(isFacingRight){
            boxCenter = (Vector2)transform.position + new Vector2(boxOffset.x, boxOffset.y); // Center of the box
        } else {
            boxCenter = (Vector2)transform.position + new Vector2(-boxOffset.x, boxOffset.y); // Center of the box
        }        
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}
