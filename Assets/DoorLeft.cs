using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLeft : MonoBehaviour
{
    private bool isMoving = false;  // Flag to check if the camera is currently moving
    private Vector3 targetPosition;  // Target position for the camera
    public GameObject _cam;
    [SerializeField] private MainCameraControl _camControl;

    void Awake()
    {
        Invoke("SetCamera", 1f);
    }

    [ContextMenu("SET CAM")]
    public void SetCamera()
    {
        GameObject cameraObject = GameObject.Find("MainCamera");
        if (cameraObject != null)
        {
            _cam = cameraObject;
            Debug.Log("Cam set");
            SetCameraScript();
        }
        else
        {
            Debug.LogError("Main Camera not found!");
        }
    }

    public void SetCameraScript()
    {
        if (_cam != null)
        {
            _camControl = _cam.GetComponent<MainCameraControl>(); // Get the MainCameraControl component
            if (_camControl != null)
            {
                Debug.Log("CamControlSet");
            }
            else
            {
                Debug.LogError("MainCameraControl script not found on camera.");
            }
        }
        else
        {
            Debug.LogError("Camera object is null!");
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    if (other.transform.position.x < transform.position.x)
        //    {
        //        // Player is entering from the left door
        //         _camControl.MoveCameraLeft();
        //    }
        //    else
        //    {
        //        // Player is entering from the right door
        //         _camControl.MoveCameraRight();
        //    }

        //    if(other.transform.position.y < transform.position.y)
        //    {
        //        _camControl.MoveCameraUp();
        //    }
        //    if(other.transform.position.y > transform.position.y)
        //    {
        //        _camControl.MoveCameraDown();
        //    }
        //}
    }
}
