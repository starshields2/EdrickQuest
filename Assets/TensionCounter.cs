using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TensionCounter : MonoBehaviour
{

    [SerializeField] public int _tension;
    [SerializeField] public int _newTension;
    public int _influence = 50;
    public int _newInfluence;
    public Slider _tensionSlider;
    public bool  tensionBreak;
    public bool  highTension;
    public bool  lowTension;

    public Companion[] companions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _tensionSlider.value = _tension;
        if(_tension <= 7)
        {
            lowTension = true;
            highTension = false;
        }
        if(_tension >= 15)
        {
            highTension = true;
            lowTension = false;
        }
        if(_tension >= 20)
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
        _tension += 1;
    }
    public void SubGeneralTension()
    {
        _tension -= 1;
    }

    public void UpdateInfluence()
    {
        _influence = _influence + _newInfluence;
    }

    public IEnumerator TensionBreakStart()
    {
        tensionBreak = false;
        _tension = 0;

        Debug.Log("Influence = " + _influence);
        foreach (Companion character in companions){
            character.AcquireAttribute();
        }

        yield return null;
    }
}
