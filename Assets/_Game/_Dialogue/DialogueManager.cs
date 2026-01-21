using System.Collections;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject[] dialogueComponents;
    private int currentDialogueIndex = -1; // Start with -1 to activate the first one when StartNPCConversation is called.

    void Start()
    {
        // Initialize your dialogue components array.
        // You can populate it with your dialogue components in the Unity Editor.
        // For example, assign your dialogue components in the Inspector.
        // dialogueComponents = GetComponentsInChildren<Dialogue>();
    }
    [ContextMenu("start")]
    public void StartNPCConversation()
    {
        currentDialogueIndex = 0;
        SetActiveDialogue();
    }
    [ContextMenu("Continue")]
    public void ContinueConversation()
    {
        if (currentDialogueIndex >= 0)
        {
            currentDialogueIndex++;

            if (currentDialogueIndex < dialogueComponents.Length)
            {
                SetActiveDialogue();
            }
            else
            {
                UnSetActiveDialogue();
            }
        }
    }

    private void SetActiveDialogue()
    {
        for (int i = 0; i < dialogueComponents.Length; i++)
        {
            dialogueComponents[i].SetActive(i == currentDialogueIndex);
        }
    }

    private void UnSetActiveDialogue()
    {
        for (int i = 0; i < dialogueComponents.Length; i++)
        {
            dialogueComponents[i].SetActive(false);
        }
    }
}
