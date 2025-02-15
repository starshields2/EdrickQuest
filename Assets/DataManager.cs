using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public Transform _cameraPosition;
    public Transform _newCamPos;
    public Transform _playerPosition;
    public Transform _savedPosition;
    
   
    void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _playerPosition = GameObject.Find("HeroKnight").transform;
        _cameraPosition = GameObject.Find("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetPlayerPosition()
    {

    }

    public void GetCameraPosition()
    {
        
    }
}
