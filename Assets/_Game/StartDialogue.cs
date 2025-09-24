using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject _dialogueHolder;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("passing");
        if(other.tag == "Player")
        {
            Debug.Log("IT'S THE PLAYE RIT'S THE PLAYER COME ON1");
            StartCoroutine(StartDialogueNow());
        }
    }

    void StartDia()
    {
       
        dialogue.StartDialogue();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("pass");
        if (other.tag == "Player")
        {
            Debug.Log("isplayer");
            _dialogueHolder.SetActive(false);
        }
    }

    IEnumerator StartDialogueNow()
    {
        _dialogueHolder.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        StartDia();

    }
}
