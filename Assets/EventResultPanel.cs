using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventResultPanel : MonoBehaviour
{
    public TextMeshProUGUI _difficulty;
    public TextMeshProUGUI _score;
    public TextMeshProUGUI _passfail;
    public OverworldManager _manager;
    public MediationDialogue _currentDialogue;
    public TensionSlider tensionSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        _manager = GameObject.Find("OverworldManager").GetComponent<OverworldManager>();
        _currentDialogue = GameObject.Find("PF_MediationDialoguePrefab").GetComponent<MediationDialogue>();
        StartScoreDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetDifficulty()
    {
        _difficulty.text = "";
        tensionSlider.SetTensionValue(_currentDialogue._diceRoll);
        
        
    }

    private void GetScore()
    {
        _score.text = "";
        tensionSlider.SetTensionValue(_manager._difficultyCheck);

    }

    private void GetPassFailValue()
    {

    }
    [ContextMenu("StartScoreDisplay")]
    public void StartScoreDisplay()
    {
        StartCoroutine(ScoreDisplay());
    }

    public void QuitPanel()
    {
        Destroy(gameObject);
    }

    public IEnumerator ScoreDisplay()
    {
        yield return new WaitForSeconds(1f);
        GetDifficulty();
        yield return new WaitForSeconds(2f);
        GetScore();
        yield return new WaitForSeconds(1f);

        if (_currentDialogue.pass == true)
        {
            _passfail.text = "STRONG";
        }
        if (_currentDialogue.pass != true)
        {
            _passfail.text = "WEAK";
        }
    }
}
