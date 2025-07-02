using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TensionCounter : MonoBehaviour
{

    [SerializeField] public float _tension;
    [SerializeField] public float _newTension;
    public int _influence = 50;
    public int _newInfluence;
    public Slider _tensionSlider;
    public Slider _TPSlider;
    public bool  tensionBreak;
    public bool  highTension;
    public bool  lowTension;
    public int _currentTetherPoints = 0;
    public int _currentMaxTetherPoints = 20;
    public int _maxTetherPoints = 20;
    public int _TPModifier = 5;

    public Companion[] companions;
    // Start is called before the first frame update

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
 
    }
    void Awake()
    {
       
    }

    public void BindSliders(Slider tension, Slider tp, Companion[] comps = null)
    {
        _tensionSlider = tension;
        _TPSlider = tp;
        if (comps != null)
            companions = comps;
    }


    public void BindCompanions(Companion yael, Companion jasper)
    {
        
    }

    void UpdateTensionValue()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        _tensionSlider.value = _tension;
        _TPSlider.value = _currentTetherPoints;
        _TPSlider.maxValue = _currentMaxTetherPoints;

        if (_tension <= 7)
        {
            //LOW TENSION
            lowTension = true;
            
            highTension = false;
            _currentMaxTetherPoints = _maxTetherPoints;
        }
        if(_tension >= 15)
        {
            //HIGH TENSION
            highTension = true;
            lowTension = false;
            _currentMaxTetherPoints = _maxTetherPoints - _TPModifier;

            if(_currentTetherPoints >= _currentMaxTetherPoints)
            {
                _currentTetherPoints = _currentMaxTetherPoints;
            }

        }
        if(_tension >= 20)
        {
            tensionBreak = true;
            StartCoroutine(TensionBreakStart());
        }

        if (_tension <= 0)
        {
            tensionBreak = true;
            StartCoroutine(TensionBreakStart());
        }


    }

    public void UpdateTension()
    {
        _tension = _tension + _newTension;
    }

    public void AddGeneralTension()
    {
        _tension += 1f;
    }
    public void SubGeneralTension()
    {
        _tension -= 1f;
    }

    public void UpdateInfluence()
    {
        _influence = _influence + _newInfluence;
    }

    public IEnumerator TensionBreakStart()
    {
        tensionBreak = false;
        _tension = 0f;

        Debug.Log("Influence = " + _influence);
        foreach (Companion character in companions){
            character.AcquireAttribute();
        }

        yield return null;
    }
}
