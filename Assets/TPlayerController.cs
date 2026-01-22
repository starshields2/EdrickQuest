using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float walkSpeed;
    private float xAxis;
    public float jumpForce = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;
    public bool isFacingRight;
    public CameraFollowObject camFollow;

    public bool jumped;
    public bool moving;
    

    // Start is called before the first frame update
    void Start()
    {

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
    void Flip()
    {
        if (xAxis < 0 && isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            isFacingRight = false;
            camFollow.CallTurn();
        }
        else if (xAxis > 0 && !isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isFacingRight = true;
            camFollow.CallTurn();
        }
    }


    // Update is called once per frame
    void Update()
    {
        GetInputs();
        Move();
        Jump();
        Flip();
        
    }

    void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    private void Move()
    {
        moving = true;
        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
        moving = Mathf.Abs(rb.velocity.x) > 0.01f; // Use a small threshold to avoid floating-point issues
    }
    void Jump()
    {
       
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
             jumped = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            Invoke("ResetJump", 0.1f);
            
        }
        if(Input.GetButtonDown("Jump") && Grounded())
        {
            jumped = true;
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
            Invoke("ResetJump", 0.1f);
        }  


    }

    void ResetJump()
    {
        jumped = false;
    }
}
