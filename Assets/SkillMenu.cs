using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillMenu : MonoBehaviour
{
 
    public int _skillPointsAC;
    public int _skillPointsCOMP;
    public int _skillPointsCOLLAB;
    public int _skillPointsCOMPET;
    public int _skillPointsAVO;
    public int _skillPoints;
    public int _SPvalue;
    public TextMeshProUGUI _availablePointsAC;
    public TextMeshProUGUI _availablePointsAVO;
    public TextMeshProUGUI _availablePointsCOMP;
    public TextMeshProUGUI _availablePointsCOLLAB;
    public TextMeshProUGUI _availablePointsCOMPET;

    void Awake()
    {
        GetSP();
    }

    [ContextMenu("GetSP")]
    public void GetSP()
    {
       _availablePointsAC.text = _skillPointsAC.ToString();
      _availablePointsAVO.text = _skillPointsAVO.ToString();
       _availablePointsCOLLAB.text = _skillPointsCOLLAB.ToString();
        _availablePointsCOMP.text = _skillPointsCOMP.ToString();
      _availablePointsCOMPET.text = _skillPointsCOMPET.ToString();
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
