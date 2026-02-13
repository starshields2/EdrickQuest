using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TensionSlider : MonoBehaviour
{
    [SerializeField] private Slider _backgroundSlider;
    [SerializeField] private ParticleSystem _sliderParticles;
    [SerializeField] private float _slideSpeedDuration = 1f;
    [SerializeField] private int _lowTension = 8;
    [SerializeField] private int _highTension = 14;
    private Slider _slider;
    private enum _tensionState { Low, Normal, High }
    private _tensionState currentTensionState;
    private Coroutine _animCoroutine;
    private Color _particleColor1 = Color.white;
    private Color _particleColor2 = Color.black;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        // Make sure background slider isn't interactable
        if (_backgroundSlider != null)
        {
            _backgroundSlider.interactable = false;
            _backgroundSlider.transition = Selectable.Transition.None;
        }
    }

    private void OnEnable()
    {
        if (_slider != null)
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
        }
    }

    private void OnDestroy()
    {
        if (_slider != null)
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }
    }

    void Start()
    {
        _sliderParticles = Instantiate(_sliderParticles, transform.position, Quaternion.identity);

        _sliderParticles.transform.SetParent(null);

        _sliderParticles.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetTensionValue((int)_slider.value + 5);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetTensionValue((int)_slider.value - 5);
        }
    }

    private void OnValueChanged(float value)
    {
        if (value <= _lowTension)
        {
            Debug.Log("Low Tension");
            currentTensionState = _tensionState.Low;
        }
        else if (value >= _highTension)
        {
            Debug.Log("High Tension");
            currentTensionState = _tensionState.High;
        }
        else
        {
            Debug.Log("Normal Tension");
            currentTensionState = _tensionState.Normal;
        }
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        // Update the slider's visuals based on the current tension state
        switch (currentTensionState)
        {
            case _tensionState.Low:
                if (_slider != null && _slider.fillRect != null)
                {
                    var img = _slider.fillRect.GetComponent<Image>();

                    if (img != null) img.color = Color.green;

                    if (_sliderParticles != null)
                    {
                        var main = _sliderParticles.main;
                        _particleColor1 = Color.green;
                        main.startColor = new ParticleSystem.MinMaxGradient(_particleColor1, _particleColor2);
                    }
                }
                break;
            case _tensionState.Normal:
                if (_slider != null && _slider.fillRect != null)
                {
                    var img = _slider.fillRect.GetComponent<Image>();

                    if (img != null) img.color = Color.yellow;

                    if (_sliderParticles != null)
                    {
                        var main = _sliderParticles.main;
                        _particleColor1 = Color.yellow;
                        main.startColor = new ParticleSystem.MinMaxGradient(_particleColor1, _particleColor2);
                    }
                }
                break;
            case _tensionState.High:
                if (_slider != null && _slider.fillRect != null)
                {
                    var img = _slider.fillRect.GetComponent<Image>();

                    if (img != null) img.color = Color.red;

                    if (_sliderParticles != null)
                    {
                        var main = _sliderParticles.main;
                        _particleColor1 = Color.red;
                        main.startColor = new ParticleSystem.MinMaxGradient(_particleColor1, _particleColor2);
                    }
                }
                break;
        }
        // Also set background slider/image to a faded variant of the main fill color
        if (_slider != null && _slider.fillRect != null)
        {
            var mainImg = _slider.fillRect.GetComponent<Image>();
            if (mainImg != null)
            {
                Color faded = new Color(mainImg.color.r, mainImg.color.g, mainImg.color.b, 0.45f);
                if (_backgroundSlider != null && _backgroundSlider.fillRect != null)
                {
                    var backgroundImg = _backgroundSlider.fillRect.GetComponent<Image>();
                    if (backgroundImg != null) backgroundImg.color = faded;
                }
            }
        }
    }

    public void SetTensionValue(int value)
    {
        if (_slider == null) return;
        if (_animCoroutine != null)
        {
            StopCoroutine(_animCoroutine);
            _animCoroutine = null;
        }
        var clamped = Mathf.Clamp(value, (int)_slider.minValue, (int)_slider.maxValue);
        _animCoroutine = StartCoroutine(AnimateSlider((float)clamped));
    }

    private IEnumerator AnimateSlider(float targetValue)
    {
        float duration = _slideSpeedDuration; // Duration of the animation
        float elapsed = 0f;
        if (_slider == null) yield break;
        float startValue = _slider.value;

        // Prepare background visuals
        if (_backgroundSlider != null)
        {
            _backgroundSlider.minValue = _slider.minValue;
            _backgroundSlider.maxValue = _slider.maxValue;
            // Ensure background slider starts at the old value
            _backgroundSlider.value = startValue;
        }

        bool increasing = targetValue > startValue;

        if (increasing)
        {
            // Increasing: background snaps to the new value, original lerps up
            if (_backgroundSlider != null)
                _backgroundSlider.value = targetValue;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                _slider.value = Mathf.SmoothStep(startValue, targetValue, t);
                _sliderParticles.transform.position = _slider.handleRect.position;
                yield return null;
            }

            _slider.value = targetValue;
        }
        else
        {
            // Decreasing: original snaps down immediately, background lerps down with same easing/duration
            _slider.value = targetValue; // Snap original immediately

            float backgroundStart = (_backgroundSlider != null) ? _backgroundSlider.value : startValue;
            elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                if (_backgroundSlider != null)
                {
                    _backgroundSlider.value = Mathf.SmoothStep(backgroundStart, targetValue, t);
                    _sliderParticles.transform.position = _backgroundSlider.handleRect.position;
                }
                yield return null;
            }

            if (_backgroundSlider != null)
                _backgroundSlider.value = targetValue;
        }

        _animCoroutine = null;
    }
}
