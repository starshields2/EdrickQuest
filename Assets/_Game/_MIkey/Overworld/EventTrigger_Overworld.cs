using System;
using UnityEngine;

public class EventTrigger_Overworld : MonoBehaviour
{
    [Header("Ink Configuration")]
    [SerializeField] private MediationDialogue _dialogueManager;
    [SerializeField] private TextAsset _storyToLoad; // Ink file

    [Header("Settings")]
    [SerializeField] private bool _triggerOnlyOnce = true;
    [SerializeField] private bool _clickAnywhereWhenInRange = false; // Allow clicking anywhere when player is in range
    [SerializeField] private bool _isRequired;
    public bool IsRequired { get => _isRequired; set => _isRequired = value; }
    private bool _hasTriggered = false;
    private bool _isPlayerInRange = false;
    
    public Action<bool> OnEventSuccess;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_hasTriggered && _triggerOnlyOnce) return;

        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = false;
        }
    }

    void Update()
    {
        if (_isPlayerInRange && Input.GetMouseButtonDown(0)) // Detect left mouse click
        {
            if (_clickAnywhereWhenInRange || IsClickOnTrigger())
            {
                ExecuteMediation();
            }
        }
    }

    private bool IsClickOnTrigger()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            return true; // The click hit this GameObject's collider
        }

        return false;
    }

    private void ExecuteMediation()
    {
        if (_dialogueManager == null || _storyToLoad == null)
        {
            Debug.LogWarning("Trigger missing Dialogue Manager or Ink File");
            return;
        }

        _hasTriggered = true;

        _dialogueManager.gameObject.SetActive(true);

        // Tell the dialogue manager this trigger initiated the story
        _dialogueManager.SetNewStory(_storyToLoad, this);

        if(_dialogueManager.backgroundCanvas != null)
            _dialogueManager.backgroundCanvas.SetActive(true);

        _dialogueManager.StartStory();
        
        Debug.Log($"Started Mediation: {_storyToLoad.name}");
    }
}