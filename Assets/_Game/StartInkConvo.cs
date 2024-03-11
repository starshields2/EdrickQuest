using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInkConvo : MonoBehaviour
{
    public GameObject inkConvo;
    public AudioSource startAud;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!inkConvo.activeSelf)
        {
            Time.timeScale = 1f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Time.timeScale = 0f;
            inkConvo.SetActive(true);
            startAud.Play();
        }
    }


}
