using UnityEngine;
using UnityEngine.UI;

public class TensionTutorial : MonoBehaviour
{
    public GameObject[] _Tutorials;
    public GameObject[] _tutCutouts;
    public Image _tutorialPanelImage;
    public MediationDialogue _medDialogue;

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
    private int _lastTutorialNum = -1; // Tracks changes to avoid running code every frame

    void Update()
    {
        // Only update if the tutorial number has actually changed
        if (_medDialogue.tutorialNum != _lastTutorialNum)
        {
            UpdateTutorialStep(_medDialogue.tutorialNum);
        }
    }

    private void UpdateTutorialStep(int stepIndex)
    {
        _lastTutorialNum = stepIndex;

        // Handle the End state (Step 5)
        if (stepIndex >= 5)
        {
            _tutorialType = TutorialType.End;
            SetAllActive(false);
            if (_tutorialPanelImage != null) _tutorialPanelImage.enabled = false;
            return;
        }

        // Update the Enum type based on the index
        _tutorialType = (TutorialType)stepIndex;

        // Loop through arrays and enable only the one matching the current index
        for (int i = 0; i < _Tutorials.Length; i++)
        {
            bool isActive = (i == stepIndex);
            
            if (_Tutorials.Length > i && _Tutorials[i] != null)
                _Tutorials[i].SetActive(isActive);

            if (_tutCutouts.Length > i && _tutCutouts[i] != null)
                _tutCutouts[i].SetActive(isActive);
        }

        if (_tutorialPanelImage != null) _tutorialPanelImage.enabled = true;
    }

    private void SetAllActive(bool state)
    {
        foreach (GameObject tut in _Tutorials) if (tut != null) tut.SetActive(state);
        foreach (GameObject cut in _tutCutouts) if (cut != null) cut.SetActive(state);
    }
}