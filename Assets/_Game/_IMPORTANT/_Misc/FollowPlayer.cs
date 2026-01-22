using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public float followSpeed = 5.0f;
    public float stopDistance = 0.1f;

    void LateUpdate()
    {
        float distanceX = Mathf.Abs(playerTransform.position.x - transform.position.x);

        if (distanceX > stopDistance)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.Lerp(transform.position.x, playerTransform.position.x, followSpeed * Time.deltaTime);
            transform.position = newPosition;
        }
    }
}
