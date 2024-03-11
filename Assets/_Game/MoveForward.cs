using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private float moveSpeed = 5.0f; // Default move speed

    private bool isAutoRunning = false;

    void Update()
    {
        if (isAutoRunning)
        {
            MovePlayerForward();
        }
    }

    // Function to start automatic movement with a specified speed
    public void StartAutoRun(float speed)
    {
        moveSpeed = speed;
        isAutoRunning = true;
    }

    // Function to stop automatic movement
    public void StopAutoRun()
    {
        isAutoRunning = false;
    }

    private void MovePlayerForward()
    {
        // Move the player forward at the specified speed
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
