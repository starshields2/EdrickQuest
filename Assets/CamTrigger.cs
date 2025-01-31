using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public Vector3 newCamPos, newPlayerPos;
    public MainCameraControl _camControl;
    public GameObject _cam;
    // Start is called before the first frame update
    void Awake()
    {
        Invoke("SetCamera", 1f);
    }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            _camControl.minPos += newCamPos;
            _camControl.maxPos += newCamPos;
            other.transform.position += newPlayerPos;
        }
    }
}
