using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public Animator _transition;
    public float _transitionTime;

    //[SerializeField] private RoomManager roomManager;

    void Awake()
    {
    //    roomManager = GameObject.Find("RoomManager").GetComponent<RoomManager>();
    }

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Contact");
        if (other.tag == "Player")
        {
            LoadScene(sceneName);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            LoadMain();
        }

    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }
    public void LoadMain()
    {
        StartCoroutine(LoadMainScene());
    }

    public void StartLoadSpecifiedScene()
    {
        StartCoroutine(LoadSpecifiedScene());
    }

    public void StartLoadPreviousScene()
    {
        StartCoroutine(LoadPreviousScene());
    }

    IEnumerator LoadLevel()
    {
        ///animation
        _transition.SetTrigger("Start");
        ///wait for stop
        yield return new WaitForSeconds(_transitionTime);
        ///load scene
        //RoomGenerationState.Instance.SaveGenerationState(RoomManager.Instance);
        SceneManager.LoadScene("TESTBATTLE");

    }

    IEnumerator LoadSpecifiedScene()
    {
        ///animation
        _transition.SetTrigger("Start");
        ///wait for stop
        yield return new WaitForSeconds(_transitionTime);
        ///load scene
        //RoomGenerationState.Instance.SaveGenerationState(RoomManager.Instance);
        SceneManager.LoadScene(sceneName);

    }

    IEnumerator LoadMainScene()
    {
        ///animation
        _transition.SetTrigger("Start");
        ///wait for stop
        yield return new WaitForSeconds(_transitionTime);
        ///load scene
        SceneManager.LoadScene("RoomTest");
    }

    IEnumerator LoadPreviousScene()
    {
        ///animation
        _transition.SetTrigger("Start");
        ///wait for stop
        yield return new WaitForSeconds(_transitionTime);
        ///load scene
        SceneManager.LoadScene(TensionSingleton.Instance.PreviousScene);
    }

    public void EndGameNow()
    {
        Application.Quit();
    }

    //public void TransitionToCombatScene()
    //{
    //    RoomGenerationState.Instance.SaveGenerationState(RoomManager.Instance);

    //    // Now load your combat scene
    //    UnityEngine.SceneManagement.SceneManager.LoadScene("CombatScene");
    //}

    //public void ReturnFromCombat()
    //{
    //    // Load the main adventure scene
    //    UnityEngine.SceneManagement.SceneManager.LoadScene("AdventureScene");
    //}
}
