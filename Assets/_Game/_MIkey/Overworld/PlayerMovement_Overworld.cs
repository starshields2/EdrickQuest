using UnityEngine;

public class PlayerMovement_Overworld : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] private Transform visualsContainer;
    public float walkSpeed = 5f;
    public float jumpForce = 10f;
    protected float xAxis;

    [Header("Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckY = 1f;
    [SerializeField] private float groundCheckX = 1f;
    [SerializeField] private LayerMask whatIsGround;
    
    public bool isFacingRight = true;
    public bool moving;
    public CameraFollow_Overworld camFollow;

    [Header("Gravity Tweaks")]
    [SerializeField] protected float fallMultiplier = 2.5f;
    [SerializeField] protected float lowJumpMultiplier = 2f;

    [Header("Slope Settings")]
    [SerializeField] private float slopeFriction = 1f; // Strength of counter-force to stop sliding

    private Vector2 slopeNormal;
    private bool onSlope;

    public virtual bool Grounded()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckY, whatIsGround) ||
               Physics2D.Raycast(groundCheck.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround) ||
               Physics2D.Raycast(groundCheck.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround);
    }

    protected virtual void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    private void CheckSlopes()
    {
        // Raycast down to find the normal of the surface
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckY + 0.5f, whatIsGround);
        
        if (hit && Grounded())
        {
            slopeNormal = hit.normal;
            // A surface is a slope if the normal's X is not 0
            onSlope = Mathf.Abs(slopeNormal.x) > 0.05f;
        }
        else
        {
            onSlope = false;
            slopeNormal = Vector2.up;
        }
    }

    protected virtual void Move()
    {
        moving = Mathf.Abs(xAxis) > 0.01f;

        if (onSlope && !Input.GetButton("Jump"))
        {
            // Calculate movement direction perpendicular to the slope normal
            // Using perpendicular vector: (-y, x)
            Vector2 slopeDirection = new Vector2(-slopeNormal.y, slopeNormal.x);
            
            // Apply velocity along the slope's surface
            rb.velocity = -slopeDirection * (xAxis * walkSpeed);
            
            // Rotate the visuals to match the slope angle
            float angle = Mathf.Atan2(slopeNormal.x, slopeNormal.y) * -Mathf.Rad2Deg;
            // Invert tilt when sprite is flipped so visuals match slope direction
            float signedAngle = isFacingRight ? angle : -angle;
            visualsContainer.localRotation = Quaternion.Euler(0, 0, signedAngle);
        }
        else
        {
            // Flat ground movement
            rb.velocity = new Vector2(xAxis * walkSpeed, rb.velocity.y);
            visualsContainer.localRotation = Quaternion.identity;

            if (Grounded() && Mathf.Abs(xAxis) < 0.01f)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    protected virtual void Flip()
    {
        if ((xAxis < 0 && isFacingRight) || (xAxis > 0 && !isFacingRight))
        {
            isFacingRight = !isFacingRight;
            // Flip the parent to handle the 180-degree turn
            transform.rotation = Quaternion.Euler(0, isFacingRight ? 0 : 180f, 0);
            camFollow?.CallTurn();
        }
    }

    protected virtual void Jump()
    {
        if (Input.GetButtonDown("Jump") && Grounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    protected virtual void ApplyGravityModifiers()
    {
        // Don't apply gravity modifiers while moving on a slope
        if (onSlope && moving) return;

        if (rb.velocity.y < 0) 
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) 
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Update()
    {
        if (MediationDialogue.IsDialogueActive) 
        {
            xAxis = 0;
            moving = false;
            visualsContainer.localRotation = Quaternion.identity;
            return; 
        }

        GetInputs();
        Jump();
        Flip();
    }

    void FixedUpdate()
    {
        if (MediationDialogue.IsDialogueActive)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        CheckSlopes();
        Move();

        // Prevent sliding on slopes when player is idle
        if (onSlope && Grounded() && Mathf.Abs(xAxis) < 0.05f)
        {
            Vector2 tangent = new Vector2(slopeNormal.y, -slopeNormal.x).normalized;
            Vector2 gravity = Physics2D.gravity * rb.gravityScale;
            float gravAlongTangent = Vector2.Dot(gravity, tangent);
            Vector2 counterForce = -gravAlongTangent * tangent * slopeFriction * rb.mass;
            rb.AddForce(counterForce, ForceMode2D.Force);
        }

        ApplyGravityModifiers();
    }
}