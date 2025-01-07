using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLeft : MonoBehaviour
{
    public Transform mainCamera;  // Camera's transform
    public float moveDuration = 1.0f;  // Duration for the camera to move (in seconds)
    private bool isMoving = false;  // Flag to check if the camera is currently moving
    private Vector3 targetPosition;  // Target position for the camera

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        if (isMoving)
        {
            // Move the camera smoothly towards the target position over time using Lerp
            mainCamera.position = Vector3.Lerp(mainCamera.position, targetPosition, Time.deltaTime / moveDuration);

            // Stop the movement when the camera is very close to the target
            if (Vector3.Distance(mainCamera.position, targetPosition) < 0.1f)
            {
                mainCamera.position = targetPosition;
                isMoving = false;  // Movement complete
            }
        }
    }

    // Trigger method to detect player's entry to a door
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Check which side of the door the player is entering from
            if (other.transform.position.x < transform.position.x)  // Player is to the left of the door
            {
                // Player is entering from the left door (we move camera to the left)
                StartCameraMoveLeft();
            }
            else  // Player is to the right of the door
            {
                // Player is entering from the right door (we move camera to the right)
                StartCameraMoveRight();
            }
        }
    }

    public void StartCameraMoveLeft()
    {

        Debug.Log("m-left");
        if (!isMoving)
        {
            targetPosition = mainCamera.position + new Vector3(17f, 0f, 0f);  // Move left
            isMoving = true;
        }
    }

    public void StartCameraMoveRight()
    {
        Debug.Log("m-right");
        if (!isMoving)
        {
            targetPosition = mainCamera.position + new Vector3(-17f, 0f, 0f);  // Move right
            isMoving = true;
        }
    }
}
