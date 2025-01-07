using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;
    public Transform thisCamera;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("HeroKnight"); // The player
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void MoveCameraRight()
    {
        thisCamera.transform.position = new Vector3(thisCamera.transform.position.x + 17, thisCamera.transform.position.y, thisCamera.transform.position.z);

    }

    public void MoveCameraLeft()
    {
        thisCamera.transform.position = new Vector3(thisCamera.transform.position.x - 17, thisCamera.transform.position.y, thisCamera.transform.position.z);
    }
}
