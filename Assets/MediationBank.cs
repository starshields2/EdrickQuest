using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediationBank : MonoBehaviour
{
    public List<GameObject> dialogues;
    public List<GameObject> restDialogues;
    public int chosenDialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetRandomMediation()
    {
        int randomChoice = Random.Range(0, dialogues.Count);
        chosenDialogue = randomChoice;

    }
}
