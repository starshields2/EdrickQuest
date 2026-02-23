using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TensionSlider : MonoBehaviour
{
    [SerializeField] private Slider _backgroundSlider;
    [Space]
    [SerializeField] private bool _useParticles = false;
    [SerializeField] private ParticleSystem _sliderParticles;
    [Space]
    [SerializeField] private float _slideSpeedDuration = 1f;
    [Space]
    [SerializeField] private int _lowTension = 8;
    [SerializeField] private int _highTension = 14;
    [Space]
    [SerializeField] private Color _lowColor = Color.green;
    [SerializeField] private Color _normalColor = Color.yellow;
    [SerializeField] private Color _highColor = Color.red;

    private Slider _slider;
    private enum _tensionState { Low, Normal, High }
    private _tensionState currentTensionState;
    private Coroutine _animCoroutine;
    private Color _particleColor1 = Color.white;
    private Color _particleColor2 = Color.black;
    private float _currentTargetValue; // Stores the most recent target value
    private Image _sliderFillImage;
    private Image _backgroundFillImage;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        // Make sure background slider isn't interactable
        if (_backgroundSlider != null)
        {
            _backgroundSlider.interactable = false;
            _backgroundSlider.transition = Selectable.Transition.None;
        }

        if (_slider != null)
        {
            _sliderFillImage = _slider.fillRect.GetComponent<Image>();
        }

        if (_backgroundSlider != null && _backgroundSlider.fillRect != null)
        {
            _backgroundFillImage = _backgroundSlider.fillRect.GetComponent<Image>();
        }

        Color interpolatedColor = GetInterpolatedColor(_slider.value);

        if (_sliderFillImage != null) _sliderFillImage.color = interpolatedColor;
        if (_backgroundFillImage != null)       
        {
            Color faded = new Color(interpolatedColor.r, interpolatedColor.g, interpolatedColor.b, 0.45f);
            _backgroundFillImage.color = faded;
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
        if (_useParticles && _sliderParticles != null)
        {
            _sliderParticles = Instantiate(_sliderParticles, transform.position, Quaternion.identity);
            _sliderParticles.transform.SetParent(null);
            _sliderParticles.Play();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetTensionValue((int)_backgroundSlider.value + 5);
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
            currentTensionState = _tensionState.Low;
        }
        else if (value >= _highTension)
        {
            currentTensionState = _tensionState.High;
        }
        else
        {
            currentTensionState = _tensionState.Normal;
        }
        UpdateVisuals();
    }

    private Color GetInterpolatedColor(float value)
    {
        // Calculate the interpolated color based on the slider's value relative to its min and max values
        float t = (value - _slider.minValue) / (_slider.maxValue - _slider.minValue);

        // Interpolate between the three colors
        if (t < 0.5f)
        {
            return Color.Lerp(_lowColor, _normalColor, t * 2f);
        }
        else
        {
            return Color.Lerp(_normalColor, _highColor, (t - 0.5f) * 2f);
        }
    }

    private void UpdateVisuals()
    {
        if (_slider == null || _slider.fillRect == null) return;

        // Get the interpolated color for the current slider value
        Color interpolatedColor = GetInterpolatedColor(_slider.value);

        // Apply the interpolated color to the slider fill
        if (_sliderFillImage != null) _sliderFillImage.color = interpolatedColor;

        // Apply the interpolated color to the background slider
        if (_backgroundSlider != null && _backgroundSlider.fillRect != null)
        {
            if (_backgroundFillImage != null)
            {
                Color faded = new Color(interpolatedColor.r, interpolatedColor.g, interpolatedColor.b, 0.45f);
                _backgroundFillImage.color = faded;
            }
        }

        // Update particle colors if enabled
        if (_sliderParticles != null && _useParticles)
        {
            var main = _sliderParticles.main;
            _particleColor1 = interpolatedColor;
            main.startColor = new ParticleSystem.MinMaxGradient(_particleColor1, _particleColor2);
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
