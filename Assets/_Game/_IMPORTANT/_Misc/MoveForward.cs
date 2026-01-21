using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveForward : MonoBehaviour
{
    private float moveSpeed = 0.5f; // Default move speed
    public Slider progress;
    public float RunProgress;
    public bool isAutoRunning = false;

    void Update()
    {
        RunProgress = this.gameObject.transform.position.x;
        progress.value = RunProgress;
        if (Input.GetKeyDown(KeyCode.R))
        {
            isAutoRunning = true;

        }

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
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
