using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInkConvo : MonoBehaviour
{
    public GameObject inkConvo;
    public AudioSource startAud;
    public GameObject prompt;
    public Dialogue _dialogue;
    public MediationBank _medBank;

    private bool playerInRange = false;

    void Awake()
    {
        _medBank = GameObject.Find("MediationsBank").GetComponent<MediationBank>();
        _medBank.GetRandomMediation();
        int chosenMediation = _medBank.chosenDialogue;
        inkConvo = _medBank.dialogues[chosenMediation];
    }

    void Update()
    {
        if (!inkConvo.activeSelf)
        {
            Time.timeScale = 1f;
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            startAud.Play();
            inkConvo.SetActive(true);
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
