using UnityEngine;

public class Companion_Overworld : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform _followTarget; 
    [SerializeField] private float _followDistance = 1.8f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _acceleration = 10f; // Smoothes the start/stop

    [Header("Detection & Slopes")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckDistance = 0.7f;

    [Header("Visuals")]
    [SerializeField] private Transform _visualsContainer;
    [Header("Slope Settings")]
    [SerializeField] private float _slopeFriction = 1f; // Strength of counter-force to stop sliding

    private bool _isFacingRight = true;
    private bool _isWaitingForPlayerToPass = false;
    private PlayerMovement_Overworld _playerScript;
    private Rigidbody2D _rb;
    private float _currentXVelocity;
    private Animator _anim;
    // Ground / slope info
    private bool _isGrounded = false;
    private Vector2 _groundNormal = Vector2.up;

    void Start()
    {
        _playerScript = FindObjectOfType<PlayerMovement_Overworld>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = _visualsContainer.GetComponent<Animator>();
        
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rb.gravityScale = 2f;

        if (_followTarget != null)
        {
            SetFacing(_followTarget.position.x >= transform.position.x);
        }
    }

    void Update()
    {
        // Don't move if dialogue is active or if we don't have a target
        if (MediationDialogue.IsDialogueActive || _playerScript == null || _followTarget == null)
        {
            _currentXVelocity = 0;
            return;
        }

        CheckIfShouldWait();
        HandleRotationAndTilt();
        HandleAnimation();
    }

    void FixedUpdate()
    {
        if (MediationDialogue.IsDialogueActive)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            return;
        }

        if (!_isWaitingForPlayerToPass)
        {
            MoveTowardsTarget();
        }
        else
        {
            // Slow down to a stop
            float targetX = Mathf.MoveTowards(_rb.velocity.x, 0, _acceleration * Time.fixedDeltaTime);
            _rb.velocity = new Vector2(targetX, _rb.velocity.y);
        }

        // Prevent sliding on slopes when companion is idle
        if (_isGrounded && Mathf.Abs(_currentXVelocity) < 0.05f)
        {
            Vector2 tangent = new Vector2(_groundNormal.y, -_groundNormal.x).normalized;
            Vector2 gravity = Physics2D.gravity * _rb.gravityScale;
            float gravAlongTangent = Vector2.Dot(gravity, tangent);
            // Apply counter-force proportional to gravity along tangent and configured friction
            Vector2 counterForce = -gravAlongTangent * tangent * _slopeFriction * _rb.mass;
            _rb.AddForce(counterForce, ForceMode2D.Force);
        }
    }

    private void MoveTowardsTarget()
    {
        float distanceToTarget = Vector2.Distance(transform.position, _followTarget.position);
        float xDifference = _followTarget.position.x - transform.position.x;

        // Only move if outside the follow distance
        if (Mathf.Abs(xDifference) > _followDistance)
        {
            float direction = Mathf.Sign(xDifference);
            float targetSpeed = direction * _moveSpeed;

            // Update facing based on movement direction
            SetFacing(direction > 0f);

            // Smoothly accelerate to the target speed
            _currentXVelocity = Mathf.MoveTowards(_rb.velocity.x, targetSpeed, _acceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Smoothly decelerate when close enough
            _currentXVelocity = Mathf.MoveTowards(_rb.velocity.x, 0, _acceleration * Time.fixedDeltaTime);
        }

        _rb.velocity = new Vector2(_currentXVelocity, _rb.velocity.y);
    }

    private void HandleRotationAndTilt()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * 0.5f, Vector2.down, _groundCheckDistance, _groundLayer);
        Debug.DrawRay(transform.position + Vector3.up * 0.5f, Vector2.down * _groundCheckDistance, Color.red);
        if (hit.collider != null)
        {
            _isGrounded = true;
            _groundNormal = hit.normal;
            float angle = Mathf.Atan2(hit.normal.x, hit.normal.y) * -Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, angle);
        }
        else
        {
            _isGrounded = false;
            _groundNormal = Vector2.up;
            // slowly return to upright when not grounded
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, transform.localRotation.eulerAngles.y, 0), Time.deltaTime * 8f);
        }
    }

    private void CheckIfShouldWait()
    {
        float directionToTarget = _followTarget.position.x - transform.position.x;

        // Wait if player is facing the companion
        bool playerFacingAway = (_playerScript.isFacingRight && directionToTarget < 0) || 
                    (!_playerScript.isFacingRight && directionToTarget > 0);

        if (playerFacingAway && Mathf.Abs(directionToTarget) < _followDistance)
        {
            _isWaitingForPlayerToPass = true;
        }
        else
        {
            _isWaitingForPlayerToPass = false;
        }

        if (_isWaitingForPlayerToPass)
        {
            // Resume if player has passed them
            bool hasPassed = (_playerScript.isFacingRight && _followTarget.position.x > transform.position.x) ||
                             (!_playerScript.isFacingRight && _followTarget.position.x < transform.position.x);

            if (hasPassed)
            {
                _isWaitingForPlayerToPass = false;
                // Face the direction of the follow target after resuming
                if (_followTarget != null)
                    SetFacing(_followTarget.position.x > transform.position.x);
            }
        }
    }

    private void SetFacing(bool facingRight)
    {
        if (_isFacingRight == facingRight) return;
        _isFacingRight = facingRight;

        // Flip the visuals container if it exists
        if (_visualsContainer != null)
        {
            Vector3 ls = _visualsContainer.localScale;
            ls.x = Mathf.Abs(ls.x) * (_isFacingRight ? 1f : -1f);
            _visualsContainer.localScale = ls;
        }
    }

    private void HandleAnimation()
    {
        if (_visualsContainer == null) return;
        _anim.SetBool("IsMoving", Mathf.Abs(_rb.velocity.x) > 0.1f);
    }
}