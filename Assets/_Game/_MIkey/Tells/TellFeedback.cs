using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TellFeedback : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _tellSprite;
    [Space]
    [SerializeField] private bool _useScreenFlash = true;
    [SerializeField] private Color _flashColor = Color.white;
    [SerializeField] private Sprite _flashSprite;
    [SerializeField] private float _flashDuration = 0.2f;
    [Space]
    [SerializeField] private bool _useScreenShake = true;
    [SerializeField] private float _shakeDuration = 0.2f;
    [SerializeField] private float _shakeMagnitude = 0.1f;
    [Space]
    [SerializeField] private bool _useAudio = true;
    [SerializeField] private AudioClip _tellSound;
    private AudioSource _audioSource;
    [Space]
    [SerializeField] private bool _useImagePopup = true;
    [SerializeField] private float _popupDuration = 0.5f;
    [SerializeField] private Vector3 _popupScale = Vector3.one * 1.5f;
    [SerializeField] private float _popupFadeDuration = 0.5f;
    [SerializeField] private Sprite _popupSprite;
    [Space]
    [SerializeField] private GameObject _shadow01;
    [SerializeField] private GameObject _shadow02;

    private void Awake()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null && _useAudio)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayTell();
        }
    }

    public void PlayTell()
    {
        if (_spriteRenderer != null && _tellSprite != null)
        {
            _spriteRenderer.sprite = _tellSprite;
            _spriteRenderer.enabled = true;
        }

        if (_shadow01 != null)
            _shadow01.SetActive(true);
        if (_shadow02 != null)
            _shadow02.SetActive(true);

        if (_useScreenFlash)
            StartCoroutine(ScreenFlashCoroutine());

        if (_useScreenShake)
            StartCoroutine(CameraShakeCoroutine());

        if (_useAudio && _tellSound != null && _audioSource != null)
            _audioSource.PlayOneShot(_tellSound);

        if (_useImagePopup && _popupSprite != null)
            StartCoroutine(ImagePopupCoroutine());
    }

    private IEnumerator ScreenFlashCoroutine()
    {
        Camera cam = Camera.main;

        // World-space flash (placed at object, behind its sprite)
        GameObject worldFlash = new GameObject("TellFlash_World");
        var worldSr = worldFlash.AddComponent<SpriteRenderer>();
        if (_spriteRenderer != null)
        {
            worldSr.sortingLayerName = _spriteRenderer.sortingLayerName;
            worldSr.sortingOrder = _spriteRenderer.sortingOrder - 1;
        }
        else
        {
            worldSr.sortingLayerName = "Default";
            worldSr.sortingOrder = -100;
        }

        if (_flashSprite != null)
        {
            worldSr.sprite = _flashSprite;
            worldFlash.transform.position = transform.position;
            worldFlash.transform.rotation = Quaternion.identity;
            worldFlash.transform.localScale = Vector3.one;
        }
        else
        {
            worldSr.sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), Vector2.one * 0.5f, 1f);
            worldFlash.transform.position = transform.position;
            worldFlash.transform.localScale = Vector3.one;
        }

        worldSr.color = new Color(_flashColor.r, _flashColor.g, _flashColor.b, 1f);

        // Screen-space flash (large sprite covering camera view, rendered on top)
        GameObject screenFlash = new GameObject("TellFlash_Screen");
        var screenSr = screenFlash.AddComponent<SpriteRenderer>();
        if (_spriteRenderer != null)
        {
            screenSr.sortingLayerName = _spriteRenderer.sortingLayerName;
            screenSr.sortingOrder = _spriteRenderer.sortingOrder + 1000;
        }
        else
        {
            screenSr.sortingLayerName = "Default";
            screenSr.sortingOrder = 1000;
        }

        // use white texture for clean full-screen flash
        var whiteSprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), Vector2.one * 0.5f, 1f);
        screenSr.sprite = whiteSprite;
        screenSr.color = new Color(_flashColor.r, _flashColor.g, _flashColor.b, 1f);

        if (cam != null && cam.orthographic)
        {
            float height = cam.orthographicSize * 2f;
            float width = height * cam.aspect;
            screenFlash.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);
            screenFlash.transform.localScale = new Vector3(width, height, 1f);
        }
        else if (cam != null)
        {
            float depth = Mathf.Abs(cam.transform.position.z - transform.position.z);
            Vector3 bl = cam.ScreenToWorldPoint(new Vector3(0, 0, depth));
            Vector3 tr = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, depth));
            Vector3 center = (bl + tr) * 0.5f;
            Vector3 size = tr - bl;
            screenFlash.transform.position = new Vector3(center.x, center.y, transform.position.z);
            screenFlash.transform.localScale = new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), 1f);
        }
        else
        {
            screenFlash.transform.position = transform.position;
            screenFlash.transform.localScale = Vector3.one * 10f;
        }

        // Fade both concurrently
        float elapsed = 0f;
        Color worldStart = worldSr.color;
        Color screenStart = screenSr.color;
        Color worldEnd = new Color(worldStart.r, worldStart.g, worldStart.b, 0f);
        Color screenEnd = new Color(screenStart.r, screenStart.g, screenStart.b, 0f);

        while (elapsed < _flashDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _flashDuration);
            worldSr.color = Color.Lerp(worldStart, worldEnd, t);
            screenSr.color = Color.Lerp(screenStart, screenEnd, t);
            yield return null;
        }

        GameObject.Destroy(worldFlash);
        GameObject.Destroy(screenFlash);
    }

    private IEnumerator CameraShakeCoroutine()
    {
        Camera cam = Camera.main;
        if (cam == null)
            yield break;

        Transform camT = cam.transform;
        Vector3 originalPos = camT.localPosition;
        float elapsed = 0f;

        while (elapsed < _shakeDuration)
        {
            elapsed += Time.deltaTime;
            Vector3 offset = Random.insideUnitSphere * _shakeMagnitude;
            camT.localPosition = originalPos + new Vector3(offset.x, offset.y, 0f);
            yield return null;
        }

        camT.localPosition = originalPos;
    }

    private IEnumerator ImagePopupCoroutine()
    {
        GameObject go = new GameObject("TellPopup");
        go.transform.position = transform.position + new Vector3(2f, 2f, 0f); // Offset above the object
        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = _popupSprite;
        sr.sortingOrder = 10000;

        Vector3 startScale = Vector3.zero;
        Vector3 targetScale = _popupScale;
        float elapsed = 0f;

        // pop-up scale in
        while (elapsed < _popupDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _popupDuration);
            go.transform.localScale = Vector3.Lerp(startScale, targetScale, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        // fade out
        elapsed = 0f;
        Color startColor = sr.color;
        startColor.a = 1f;
        Color endColor = startColor;
        endColor.a = 0f;

        while (elapsed < _popupFadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _popupFadeDuration);
            sr.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        GameObject.Destroy(go);
    }
}
