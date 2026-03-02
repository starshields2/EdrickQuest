using UnityEngine;

public class EventTrigger_Overworld : MonoBehaviour
{
    [Header("Ink Configuration")]
    [SerializeField] private MediationDialogue dialogueManager;
    [SerializeField] private TextAsset storyToLoad; // The specific Ink file for this trigger

    [Header("Settings")]
    public bool triggerOnlyOnce = true;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered && triggerOnlyOnce) return;

        if (other.CompareTag("Player"))
        {
            ExecuteMediation();
        }
    }

    private void ExecuteMediation()
    {
        if (dialogueManager == null || storyToLoad == null)
        {
            Debug.LogWarning("Trigger missing Dialogue Manager or Ink File!");
            return;
        }

        hasTriggered = true;

        dialogueManager.SetNewStory(storyToLoad);

        dialogueManager.gameObject.SetActive(true);
        if(dialogueManager.backgroundCanvas != null)
            dialogueManager.backgroundCanvas.SetActive(true);

        dialogueManager.StartStory();
        
        Debug.Log($"Started Mediation: {storyToLoad.name}");
    }
}