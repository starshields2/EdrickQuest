using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TensionTutorial : MonoBehaviour
{
    public GameObject[] _Tutorials;
    public GameObject[] _tutCutouts;
    public Image _tutorialPanelImage;
    public MediationDialogue _medDialogue;
    public Button[] _tutorialButtons;

    public enum TutorialType
    {
        Start = 0,
        Highlight = 1,
        TensionBar = 2,
        Tells = 3,
        Value = 4,
        End = 5
    }

    public TutorialType _tutorialType = TutorialType.Start;
    private int _lastTutorialNum = -1;
    private Button _cachedContinueButton;

    private void OnEnable()
    {
        LinkHandler.OnHoverOnLinkEvent += GetToolTipInfo;

        foreach (Button button in _tutorialButtons)
        {
            button.onClick.AddListener(EnableContinueButton);
        }
    }

    private void OnDisable()
    {
        LinkHandler.OnHoverOnLinkEvent -= GetToolTipInfo;

        foreach (var button in _tutorialButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void GetToolTipInfo(string keyword, Vector3 mousePosition)
    {
        if (keyword == "Ritual")
        {
            _tutorialButtons[0].gameObject.SetActive(true);

            LinkHandler.OnHoverOnLinkEvent -= GetToolTipInfo;
        }
    }

    public void TellClicked()
    {
        _tutorialButtons[1].gameObject.SetActive(true);
    }

    void Update()
    {
        if (_medDialogue.tutorialNum != _lastTutorialNum)
        {
            UpdateTutorialStep(_medDialogue.tutorialNum);
        }
    }

    private void UpdateTutorialStep(int stepIndex)
    {
        _lastTutorialNum = stepIndex;

        if (stepIndex >= 5)
        {
            _tutorialType = TutorialType.End;
            SetAllActive(false);
            if (_tutorialPanelImage != null) _tutorialPanelImage.enabled = false;
            return;
        }

        _tutorialType = (TutorialType)stepIndex;

        for (int i = 0; i < _Tutorials.Length; i++)
        {
            bool isActive = (i == stepIndex);
            if (_Tutorials[i] != null) _Tutorials[i].SetActive(isActive);
            if (_tutCutouts.Length > i && _tutCutouts[i] != null) _tutCutouts[i].SetActive(isActive);
        }

        if (_tutorialPanelImage != null) _tutorialPanelImage.enabled = true;

        StopAllCoroutines(); 
        StartCoroutine(DisableContinueButtonEndOfFrame());
    }

    private IEnumerator DisableContinueButtonEndOfFrame()
    {
        // Wait until the very end of the frame so other scripts have finished enabling the button.
        yield return new WaitForEndOfFrame();

        if (_cachedContinueButton == null)
        {
            GameObject go = GameObject.Find("EDRICKSPEAKBUTTON -Continue(Clone)");
            if (go != null) _cachedContinueButton = go.GetComponent<Button>();
        }

        if (_cachedContinueButton != null)
        {
            _cachedContinueButton.interactable = false;
            Debug.Log("Continue Button Disabled via Coroutine");
        }
    }

    private void SetAllActive(bool state)
    {
        foreach (GameObject tut in _Tutorials) if (tut != null) tut.SetActive(state);
        foreach (GameObject cut in _tutCutouts) if (cut != null) cut.SetActive(state);
    }

    private void EnableContinueButton()
    {
        if (_cachedContinueButton != null)
        {
            _cachedContinueButton.interactable = true;
            Debug.Log("Enabled Continue Button");
        }
    }
}