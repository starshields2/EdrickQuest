using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayerMovement : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    public float walkSpeed;
    protected float xAxis;
    public float jumpForce = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;
    public bool isFacingRight;
    public OverworldCameraFollow camFollow;

    public bool jumped;
    public bool moving;
    [SerializeField] protected float fallMultiplier = 2.5f; // Multiplier for fast falling
    [SerializeField] protected float lowJumpMultiplier = 2f; // Multiplier for variable jump height
    [SerializeField] protected float fastFallSpeed = 5f; // Speed for fast falling

    public virtual bool Grounded()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckY, whatIsGround) ||
               Physics2D.Raycast(groundCheck.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround) ||
               Physics2D.Raycast(groundCheck.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround);
    }

    protected virtual void Flip()
    {
        if (xAxis < 0 && isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            isFacingRight = false;
            camFollow?.CallTurn();
        }
        else if (xAxis > 0 && !isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isFacingRight = true;
            camFollow?.CallTurn();
        }
    }

    protected virtual void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    protected virtual void Move()
    {
        moving = true;

        // Check for slopes using raycasts
        Vector2 rayOrigin = rb.position;
        Vector2 rayDirection = Vector2.down;
        float rayDistance = groundCheckY + 2f; // Slightly longer than ground check

        RaycastHit2D slopeHit = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, whatIsGround);

        if (slopeHit && Mathf.Abs(slopeHit.normal.x) > 0.01f) // Detect slope
        {
            Vector2 slopeDirection = new Vector2(-slopeHit.normal.y, slopeHit.normal.x); // Perpendicular to the slope
            rb.velocity = slopeDirection * (walkSpeed * xAxis);

            // Calculate the angle of the slope and rotate the player
            float slopeAngle = slopeHit.transform.eulerAngles.z;
            transform.GetChild(0).rotation = Quaternion.Euler(0, isFacingRight ? 0 : 180f, slopeAngle);
        }
        else
        {
            rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);

            // Reset rotation on flat ground
            transform.rotation = Quaternion.Euler(0, isFacingRight ? 0 : 180f, 0);
        }

        moving = Mathf.Abs(rb.velocity.x) > 0.01f; // Use a small threshold to avoid floating-point issues
    }

    protected virtual void Jump()
    {
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            jumped = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            Invoke("ResetJump", 0.1f);
        }
        if (Input.GetButtonDown("Jump") && Grounded())
        {
            jumped = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Invoke("ResetJump", 0.1f);
        }
    }

    protected virtual void ResetJump()
    {
        jumped = false;
    }

    protected virtual void ApplyGravityModifiers()
    {
        if (rb.velocity.y < 0) // Falling
        {
            float gravityAdjustment = Input.GetKey(KeyCode.DownArrow) ? (fallMultiplier + fastFallSpeed) : (fallMultiplier - 1);
            rb.velocity += Vector2.up * Physics2D.gravity.y * gravityAdjustment * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) // Variable jump height
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public virtual float GetXAxis()
    {
        return xAxis;
    }

    void Update()
    {
        GetInputs();
        Move();
        Jump();
        Flip();
        ApplyGravityModifiers();
    }
}
