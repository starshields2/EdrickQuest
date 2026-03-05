using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverworldManager : MonoBehaviour
{
    public int _publicTension;
    public int _difficultyCheck = 10;
    public Slider _overworldTension;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _overworldTension.value = _publicTension;
        _difficultyCheck = 5 + (_publicTension % 2); 
    }
}
