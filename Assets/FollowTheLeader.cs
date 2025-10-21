using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheLeader : MonoBehaviour
{
    public float actionDelay = 0.5f;
    private float xAxis;
    public float walkSpeed;
    public TPlayerController leader;
    [SerializeField] private Rigidbody2D rb;
    public float jumpForce = 7f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;
    public bool isFacingRight;
    public bool flipping;
    public bool lastPlayerFacingRight;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame


        void Update()
        {
        GetInputs();
  
            if (leader.isFacingRight != lastPlayerFacingRight)
            {
                StartFlip();
                lastPlayerFacingRight = leader.isFacingRight;
            }

        if (leader.jumped)
        {
            Debug.Log("Delayed Jump.");
            StartCoroutine(DelayedJump());
        }

        if (leader.moving)
        {
            StartCoroutine(DelayedMove());
        }
        else if (!leader.moving)
        {
            Stop();
        }
        }


    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }
    public bool Grounded()
    {
        if (Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundCheck.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround)
            || Physics2D.Raycast(groundCheck.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround))

        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void StartFlip()
    {
        if (!flipping)
        {
            StartCoroutine(DelayedFlip());
        }
        
    }

    void Flip()
    {
        if (!leader.isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            isFacingRight = false;
            
        }
        else if (leader.isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isFacingRight = true;
            
        }
    }

    private IEnumerator DelayedFlip()
    {
        flipping = true;
        yield return new WaitForSeconds(actionDelay);
        Flip();
        flipping = false;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply upward velocity
    }


    private IEnumerator DelayedJump()
    {
        yield return new WaitForSeconds(actionDelay);
        Jump();
    }

    private IEnumerator DelayedMove()
    {
        yield return new WaitForSeconds(actionDelay);
        Move();

    }

    private void Move()
    {
        float direction = leader.isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(walkSpeed * direction, rb.velocity.y);
    }


    private void Stop()
    {
        rb.velocity = new Vector2(walkSpeed * 0, rb.velocity.y);
    }



}
