using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionManagement : MonoBehaviour
{
    public PlayetCutsceneMovement _player;
    public GameObject[] cutsceneDialogue;
    public Dialogue _currentDialogue;
    public float waitTime;
    public float waitBetweenLinesTime;
    public CanvasGroup blackScreen;
    public CanvasGroup overworldMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("StartGame")]
    public void StartOpeningCutscene()
    {
        StartCoroutine(OpeningCutscenePlayer());
    }

    public IEnumerator OpeningCutscenePlayer()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("started cutscene");
        cutsceneDialogue[0].SetActive(true);
        _currentDialogue = cutsceneDialogue[0].GetComponent<Dialogue>();
        _currentDialogue.StartDialogue();
        yield return new WaitForSeconds(waitTime);
        cutsceneDialogue[0].SetActive(false);
        yield return new WaitForSeconds(waitBetweenLinesTime);

        Debug.Log("started cutscene 2");
        cutsceneDialogue[1].SetActive(true);
        _currentDialogue = cutsceneDialogue[1].GetComponent<Dialogue>();
        _currentDialogue.StartDialogue();
        yield return new WaitForSeconds(waitTime);
        cutsceneDialogue[1].SetActive(false);
        yield return new WaitForSeconds(waitBetweenLinesTime);

        Debug.Log("started cutscene");
        cutsceneDialogue[2].SetActive(true);
        _currentDialogue = cutsceneDialogue[2].GetComponent<Dialogue>();
        _currentDialogue.StartDialogue();
        yield return new WaitForSeconds(waitTime);
        cutsceneDialogue[2].SetActive(false);
        yield return new WaitForSeconds(waitBetweenLinesTime);

        Debug.Log("started cutscene");
        cutsceneDialogue[3].SetActive(true);
        _currentDialogue = cutsceneDialogue[3].GetComponent<Dialogue>();
        _currentDialogue.StartDialogue();
        yield return new WaitForSeconds(waitTime);
        cutsceneDialogue[3].SetActive(false);
        yield return new WaitForSeconds(waitBetweenLinesTime);

        blackScreen.alpha = 0;
        overworldMenu.alpha = 1;
        _player._startMove = true;
    }
}
