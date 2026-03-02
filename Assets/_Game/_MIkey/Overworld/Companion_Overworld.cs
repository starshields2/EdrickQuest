using UnityEngine;

public class Companion_Overworld : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform followTarget; 
    public float followDistance = 1.8f;
    public float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f; // Smoothes the start/stop

    [Header("Detection & Slopes")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance = 0.7f;
    [SerializeField] private Transform visualsContainer;

    private bool isWaitingForPlayerToPass = false;
    private PlayerMovement_Overworld playerScript;
    private Rigidbody2D rb;
    private float currentXVelocity;

    void Start()
    {
        playerScript = FindObjectOfType<PlayerMovement_Overworld>();
        rb = GetComponent<Rigidbody2D>();
        
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 2f; 
    }

    void Update()
    {
        // Don't move if dialogue is active or if we don't have a target
        if (MediationDialogue.IsDialogueActive || playerScript == null || followTarget == null)
        {
            currentXVelocity = 0;
            return;
        }

        CheckIfShouldWait();
        HandleRotationAndTilt();
    }

    void FixedUpdate()
    {
        if (MediationDialogue.IsDialogueActive)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        if (!isWaitingForPlayerToPass)
        {
            MoveTowardsTarget();
        }
        else
        {
            // Slow down to a stop
            float targetX = Mathf.MoveTowards(rb.velocity.x, 0, acceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector2(targetX, rb.velocity.y);
        }
    }

    private void MoveTowardsTarget()
    {
        float distanceToTarget = Vector2.Distance(transform.position, followTarget.position);
        float xDifference = followTarget.position.x - transform.position.x;

        // Only move if outside the follow distance
        if (Mathf.Abs(xDifference) > followDistance)
        {
            float direction = Mathf.Sign(xDifference);
            float targetSpeed = direction * moveSpeed;

            // Smoothly accelerate to the target speed
            currentXVelocity = Mathf.MoveTowards(rb.velocity.x, targetSpeed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Smoothly decelerate when close enough
            currentXVelocity = Mathf.MoveTowards(rb.velocity.x, 0, acceleration * Time.fixedDeltaTime);
        }

        rb.velocity = new Vector2(currentXVelocity, rb.velocity.y);
    }

    private void HandleRotationAndTilt()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        if (hit.collider != null && visualsContainer != null)
        {
            float angle = Mathf.Atan2(hit.normal.x, hit.normal.y) * -Mathf.Rad2Deg;
            float signedAngle = angle;
            visualsContainer.localRotation = Quaternion.Euler(0, visualsContainer.localRotation.eulerAngles.y, signedAngle);
            
            // Lerp for the tilt
            Quaternion targetRot = Quaternion.Euler(0, visualsContainer.localRotation.eulerAngles.y, angle);
            visualsContainer.localRotation = Quaternion.Lerp(visualsContainer.localRotation, targetRot, Time.deltaTime * 10f);
        }
    }

    private void CheckIfShouldWait()
    {
        float directionToTarget = followTarget.position.x - transform.position.x;

        // Wait if player is facing the companion
        bool playerFacingAway = (playerScript.isFacingRight && directionToTarget < 0) || 
                                (!playerScript.isFacingRight && directionToTarget > 0);

        if (playerFacingAway)
        {
            isWaitingForPlayerToPass = true;
        }

        if (isWaitingForPlayerToPass)
        {
            // Resume if player has passed them
            bool hasPassed = (playerScript.isFacingRight && followTarget.position.x > transform.position.x) ||
                             (!playerScript.isFacingRight && followTarget.position.x < transform.position.x);

            if (hasPassed)
            {
                isWaitingForPlayerToPass = false;
                visualsContainer.GetComponent<SpriteRenderer>().flipX = !playerScript.isFacingRight;
            }
        }
    }
}