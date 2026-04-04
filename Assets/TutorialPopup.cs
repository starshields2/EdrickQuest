using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialPopup : MonoBehaviour
{

    public List<string> TutorialText;
    public TextMeshProUGUI tutorialText;
    public int tutNumb;
    public CanvasGroup _tutorialGroup;
    [Space(10)]
    public GameObject _tuorialPanelStencil;
    public GameObject _tutorialCutout;
    public GameObject _mapButtonCutout;
    public GameObject _journalButtonCutout;
    [Space(10)]
    public Button _menuButton;
    public Button _mapButton;
    public Button _journalButton;
    private PlayerMovement_Overworld _playerMovement;

    private void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement_Overworld>();
    }
    public void StartDisplayTutorial()
    {
        StartCoroutine(TutorialDisplay());
    }
    
    private IEnumerator TutorialDisplay()
    {
        yield return null;
        _tutorialGroup.alpha = 1;
        DisplayTutText();

        switch (tutNumb)
        {
            case 5:
                _mapButtonCutout.SetActive(true);
                _tutorialCutout.SetActive(true);
                _tuorialPanelStencil.SetActive(true);

                _journalButton.enabled = false;
                _menuButton.enabled = false;

                _playerMovement.DisableMovement();
                break;
            case 8:                
                _journalButtonCutout.SetActive(true);
                _tutorialCutout.SetActive(true);
                _tuorialPanelStencil.SetActive(true);

                _mapButton.enabled = false;
                _menuButton.enabled = false;

                _playerMovement.DisableMovement();
                break;
        }
    }
    public void DisplayTutText()
    {
        tutorialText.text = TutorialText[tutNumb];
    }

    public void DestroyTutText()
    {
        _tutorialGroup.alpha = 0;
    }

    public void HideCutOuts()
    {
        _mapButton.enabled = true;
        _journalButton.enabled = true;
        _menuButton.enabled = true;

        _mapButtonCutout.SetActive(false);
        _journalButtonCutout.SetActive(false);
        _tutorialCutout.SetActive(false);
        _tuorialPanelStencil.SetActive(false);
        _tutorialGroup.alpha = 0;
    }
}
