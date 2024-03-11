using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShufflePartyDialogue : MonoBehaviour
{
    [Header("Ink Components")]
    public List<GameObject> Conversations;

    private List<GameObject> activeConversations = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {

    }

    public void ShuffleDialogue()
    {
        // Deactivate all active conversations
        foreach (GameObject conversation in activeConversations)
        {
            conversation.SetActive(false);
        }
        activeConversations.Clear(); // Clear the list of active conversations

        if (Conversations.Count > 0)
        {
            int randomIndex = Random.Range(0, Conversations.Count);
            GameObject selectedInk = Conversations[randomIndex];
            selectedInk.SetActive(true);
            Conversations.RemoveAt(randomIndex);

            activeConversations.Add(selectedInk); // Add the selected conversation to the list of active conversations
        }
        else
        {
            Debug.Log("Out of stuff to do.");
        }
    }
}
