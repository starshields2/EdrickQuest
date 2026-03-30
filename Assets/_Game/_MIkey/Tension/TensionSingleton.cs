using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TensionSingleton : MonoBehaviour
{
private int _tensionLevel = 0;
public int TensionLevel
{
    get { return _tensionLevel; }
    set { _tensionLevel = value; }
}
    public static TensionSingleton Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void ClampTension()
    {
        _tensionLevel = Mathf.Clamp(_tensionLevel, 0, 23);
    }
}
