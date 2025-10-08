using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject _dialogueHolder;
    public GameObject _cutSceneHolder;
    public int ID;

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
            if(_dialogueHolder != null && dialogue != null)
            {
                StartCoroutine(StartDialogueNow());
            }
            

            if(_cutSceneHolder !=null)
            {
                StartCScene();
            }
        }
    }

    void StartDia()
    {
       
        dialogue.StartDialogue();
    }

    void StartCScene()
    {
        _cutSceneHolder.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("pass");
        if (other.tag == "Player")
        {

            Debug.Log("isplayer");
            if(_dialogueHolder != null)
            {
            _dialogueHolder.SetActive(false);
            }
            
        }
    }

    IEnumerator StartDialogueNow()
    {
        _dialogueHolder.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        StartDia();

    }
}
