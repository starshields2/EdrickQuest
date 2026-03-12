using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayetCutsceneMovement : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed = 1.0f;
    public Transform _Player;
    // The target (cylinder) position.
    public Transform target;
    public bool _startMove;

    void Awake()
    {
    
    }

    void Update()
    {
        if (_startMove)
        {
            // Move our position a step closer to the target.
            float step = speed * Time.deltaTime; // calculate distance to move
            _Player.position = Vector3.MoveTowards(transform.position, target.position, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                _startMove = false;
                // Reset the target position to the original object position.
                target.position *= -1.0f;
            }
        }
       
    }
}
