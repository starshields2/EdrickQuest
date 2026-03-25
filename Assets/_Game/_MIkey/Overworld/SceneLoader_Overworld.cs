using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader_Overworld : MonoBehaviour
{
    public string sceneName;
    public Animator _transition;
    public float _transitionTime;
    private SpriteRenderer _sr;
    private SpriteRenderer _srChild;
    private BoxCollider2D _bc;
    private bool _isPlayerInRange = false;
    private EventTrigger_Overworld[] _eventTriggers;
    private int _requiredCount = 0;
    private int _resolvedSuccessCount = 0;
    private bool _allRequiredSuccessful = true;

    //[SerializeField] private RoomManager roomManager;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _srChild = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _bc = GetComponent<BoxCollider2D>();

        _eventTriggers = FindObjectsOfType<EventTrigger_Overworld>();

        foreach (var trigger in _eventTriggers)
        {
            if (trigger.IsRequired)
            {
                _requiredCount++;
                var t = trigger; // capture for closure
                t.OnEventSuccess += (bool success) => CheckRequiredTriggers(t, success);
            }
        }
    }

    private void Update()
    {
        if (_isPlayerInRange && Input.GetMouseButtonDown(0) && IsClickOnTrigger())
        {
            if (_requiredCount == 0 || (_resolvedSuccessCount >= _requiredCount && _allRequiredSuccessful))
            {
                StartCoroutine(LoadSpecifiedScene());
            }
            else
            {
                Debug.Log("Cannot pass: required events unresolved or failed.");
            }
        }
    }

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _isPlayerInRange = true;
            StartCoroutine(Fade(0.7f));
            StartLoadSpecifiedScene();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _isPlayerInRange = false;
            StartCoroutine(Fade(0f));
        }
    }

    public void StartLoadSpecifiedScene()
    {
        if (_requiredCount == 0 || (_resolvedSuccessCount >= _requiredCount && _allRequiredSuccessful))
        {
            StartCoroutine(LoadSpecifiedScene());
        }
        else
        {
            Debug.Log("Cannot pass: required events unresolved or failed.");
        }
    }

    private void CheckRequiredTriggers(EventTrigger_Overworld trigger, bool success)
    {
        if (success)
        {
            _resolvedSuccessCount++;
        }
        else
        {
            _allRequiredSuccessful = false;
        }
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float elapsedTime = 0f;
        float fadeTime = 0.1f;
        Color startColor = _sr.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        while (elapsedTime < fadeTime)
        {
            _sr.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeTime);
            _srChild.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _sr.color = targetColor;
        _srChild.color = targetColor;
    }

    private IEnumerator LoadSpecifiedScene()
    {
        ///animation
        _transition.SetTrigger("Start");
        ///wait for stop
        yield return new WaitForSeconds(_transitionTime);
        ///load scene
        //RoomGenerationState.Instance.SaveGenerationState(RoomManager.Instance);
        SceneManager.LoadScene(sceneName);

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
}
