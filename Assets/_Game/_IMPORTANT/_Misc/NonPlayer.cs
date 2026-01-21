using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayer : MonoBehaviour
{
    public GameObject dialog;
    public Dialogue dialogQue;
    public GameObject prompt;

    public bool dialogueOpen = false; // Flag to track if the dialogue is open or closed

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // dialog.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            prompt.SetActive(true);
            Debug.Log("Player reached");
            if (!dialogueOpen && Input.GetKeyDown(KeyCode.R)) // Check if the dialogue is not already open
            {
                Debug.Log("R Pressed");
                StartCoroutine(GoDialogue());
                
            }
        }
    }

    public IEnumerator GoDialogue()
    {
        dialogueOpen = true; // Set the flag to indicate the dialogue is open
        dialog.SetActive(true);
      

        yield return new WaitForSeconds(1f);
        dialogueOpen = false;
    }
}
