using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float walkSpeed;
    private float xAxis;
    private float jumpForce = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;
    private bool isFacingRight;

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
        if(xAxis < 0)
        {
            if (isFacingRight)
            {
Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
                isFacingRight = !isFacingRight;
            }
            
        }
        else if(xAxis> 0)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
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
        rb.velocity = new Vector2(walkSpeed * xAxis, rb.velocity.y);
    }
    void Jump()
    {
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        if(Input.GetButtonDown("Jump") && Grounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        }
    }
}
