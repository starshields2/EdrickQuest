using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class EventTrigger_Overworld : MonoBehaviour
{
    [Header("Ink Configuration")]
    [SerializeField] private MediationDialogue _dialogueManager;
    [SerializeField] private TextAsset _storyToLoad; // Ink file

    [Header("Settings")]
    [SerializeField] private bool _triggerOnlyOnce = true;
    [SerializeField] private bool _clickAnywhereWhenInRange = true; // Allow clicking anywhere when player is in range
    [SerializeField] private bool _isRequired;
    [SerializeField] private bool _hideExcaimationOnTriggerExit = true;
    public bool IsRequired { get => _isRequired; set => _isRequired = value; }
    private bool _hasTriggered = false;
    private bool _isPlayerInRange = false;
    private SpriteRenderer _sr;
    private SpriteRenderer _srChild;
    public Action<bool> OnEventSuccess;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _srChild = transform.GetChild(0).GetComponent<SpriteRenderer>();
        StopAllCoroutines();
        StartCoroutine(Fade(0f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isPlayerInRange)
        {
            _isPlayerInRange = true;
            StopAllCoroutines();
            StartCoroutine(Fade(1f));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!_hideExcaimationOnTriggerExit) return;

        if (other.CompareTag("Player") && _isPlayerInRange)
        {
            _isPlayerInRange = false;
            StopAllCoroutines();
            StartCoroutine(Fade(0f));
        }
    }

    void Update()
    {
        if(_triggerOnlyOnce && _hasTriggered) return;

        if (_isPlayerInRange && Input.GetMouseButtonDown(0) && _clickAnywhereWhenInRange) // Detect left mouse click
        {
            if (!_dialogueManager.isActiveAndEnabled)
            {
                ExecuteMediation();
            }
        }
    }

    private void OnMouseDown()
    {
        if(_triggerOnlyOnce && _hasTriggered) return;

        if (_isPlayerInRange && !_dialogueManager.isActiveAndEnabled && !_clickAnywhereWhenInRange)
        {
            ExecuteMediation();
        }
    }

    // private bool IsClickOnTrigger()
    // {
    //     Vector3 mousePos = Input.mousePosition;
    //     Ray ray = Camera.main.ScreenPointToRay(mousePos);
    //     RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

    //     if (hit.collider != null && hit.collider.gameObject == gameObject)
    //     {
    //         return true; // The click hit this GameObject's collider
    //     }

    //     return false;
    // }

    private void ExecuteMediation()
    {
        if (_dialogueManager == null || _storyToLoad == null)
        {
            Debug.LogWarning("Trigger missing Dialogue Manager or Ink File");
            return;
        }

        _hasTriggered = true;

        if(_triggerOnlyOnce)
        {
            // Disable the collider to prevent future triggers
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
                _srChild.gameObject.SetActive(false); // Hide exclamation mark
            }
        }

        _dialogueManager.gameObject.SetActive(true);

        // Tell the dialogue manager this trigger initiated the story
        _dialogueManager.SetNewStory(_storyToLoad, this);

        if(_dialogueManager.backgroundCanvas != null)
            _dialogueManager.backgroundCanvas.SetActive(true);

        _dialogueManager.StartStory();

        StopAllCoroutines();
        StartCoroutine(Fade(0f));
        
        Debug.Log($"Started Mediation: {_storyToLoad.name}");
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float elapsedTime = 0f;
        float fadeTime = 0.1f;
        Color startColor = _srChild.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        while (elapsedTime < fadeTime)
        {
            //_sr.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeTime);
            _srChild.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //_sr.color = targetColor;
        _srChild.color = targetColor;
    }
}