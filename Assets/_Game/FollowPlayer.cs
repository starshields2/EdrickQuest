using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's Transform component
    public float followSpeed = 5.0f; // Adjust this value to control the follow speed
    public float stopDistance = 1.0f; // The distance at which the following stops

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > stopDistance)
        {
            Vector3 scale = transform.localScale;

            if (playerTransform.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1;
                transform.Translate(followSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                scale.x = Mathf.Abs(scale.x);
            }

            transform.localScale = scale;
        }
    }
}
