using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInkConvo : MonoBehaviour
{
    public GameObject inkConvo;
    public AudioSource startAud;
    public GameObject prompt;
    public Dialogue _dialogue;

    private bool playerInRange = false;

    void Update()
    {
        if (!inkConvo.activeSelf)
        {
            Time.timeScale = 1f;
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 0f;
            inkConvo.SetActive(true);
            startAud.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            prompt.SetActive(true);
            _dialogue.StartDialogue();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            prompt.SetActive(false);
        }
    }
}
