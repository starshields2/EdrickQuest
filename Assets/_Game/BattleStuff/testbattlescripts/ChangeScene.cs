using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeScene : MonoBehaviour {

    public SceneLoader scene;
	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            scene.LoadScene("TESTBATTLE");
        }
    }
}
