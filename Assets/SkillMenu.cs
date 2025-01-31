using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillMenu : MonoBehaviour
{
    public int _skillPoints;
    public int _SPvalue;
    public TextMeshProUGUI _availablePointsText;

    void Awake()
    {
        GetSP();
    }

    public void GetSP()
    {
        _availablePointsText.text = _skillPoints.ToString();
    }

    public void SubSP(int _SPvalue)
    {
        _skillPoints -= _SPvalue;
        GetSP();
    }
    public void AddSP(int _SPvalue)
    {
        _skillPoints += _SPvalue;
        GetSP();
    }


}
