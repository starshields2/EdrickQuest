using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverworldManager : MonoBehaviour
{
    //public int _publicTension;
    public int _difficultyCheck = 10;
    public int _modifier = 1;
    public Slider _overworldTension;
    public Image _tensionSliderBGBlur;
    public float _tensionMeshRotateSpeed;
    public Rotator _rotatorCS;
    public MeshRenderer[] TensionMesh;
    public Material[] TensionMats;

    public bool highTension;
    public bool lowTension; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _overworldTension.value = TensionSingleton.Instance.TensionLevel;
        TensionSingleton.Instance.ClampTension();
        if (TensionSingleton.Instance.TensionLevel > 12)
        {
            HighTensionVisual();
        }
        if(TensionSingleton.Instance.TensionLevel < 5)
        {
            LowTensionVisual();
        }
        if (highTension)
        {
            _modifier = TensionSingleton.Instance.TensionLevel / 2;
        }
        if (lowTension)
        {
            _modifier = TensionSingleton.Instance.TensionLevel / 4;
        }
        _difficultyCheck = (TensionSingleton.Instance.TensionLevel) + _modifier; 
    }

    [ContextMenu("HighTension")]
    public void HighTensionVisual()
    {
        if (!highTension)
        {
            lowTension = false;
            highTension = true;
            _rotatorCS.yAngle = 0.7f;
            _tensionSliderBGBlur.color = Color.red;
            foreach (MeshRenderer thread in TensionMesh)
            {
                thread.material = TensionMats[1];
            }

        }
        
    }

    [ContextMenu("LowTension")]
    public void LowTensionVisual()
    {
        if (!lowTension)
        {
            lowTension = true;
            highTension = false;
            _rotatorCS.yAngle = 0.1f;
            _tensionSliderBGBlur.color = Color.blue;
            foreach (MeshRenderer thread in TensionMesh)
            {
                thread.material = TensionMats[0];
            }

        }
        
    }

    
}
