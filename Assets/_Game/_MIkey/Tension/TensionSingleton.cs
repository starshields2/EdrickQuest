using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TensionSingleton : MonoBehaviour
{
    private int _tensionLevel = 0;
    public int TensionLevel
    {
        get { return _tensionLevel; }
        set { _tensionLevel = value; }
    }

    private string _previousScene;
    public string PreviousScene
    {
        get { return _previousScene; }
        set { _previousScene = value; }
    }

    public static TensionSingleton Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable() 
    {
        SceneManager.activeSceneChanged += SetPreviousScene;
    }

    private void OnDisable() 
    {
        SceneManager.activeSceneChanged -= SetPreviousScene;
    }

    public void ClampTension()
    {
        _tensionLevel = Mathf.Clamp(_tensionLevel, 0, 23);
    }

    public void SetPreviousScene()
    {
        _previousScene = SceneManager.GetActiveScene().name;
    }
    public void SetPreviousScene(Scene previousScene, Scene newScene)
    {
        _previousScene = previousScene.name;
    }
}
