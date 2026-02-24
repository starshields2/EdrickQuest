using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TensionTutorial : MonoBehaviour
{
    public GameObject[] _Tutorials;
    public MediationDialogue _medDialogue;
    public enum TutorialType
        
    {
        Start,
        Highlight,
        TensionBar,
        Tells,
        End
    }

    public TutorialType _tutorialType = TutorialType.Start;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_medDialogue.tutorialNum == 0)
        {
            _tutorialType = TutorialType.Start;
            _Tutorials[0].SetActive(true);

            // Disable all other _Tutorials game objects
            for (int i = 1; i < _Tutorials.Length; i++)
            {
                if (i != 0) // Skip index 0
                {
                    _Tutorials[i].SetActive(false);
                }
            }
        }
        if (_medDialogue.tutorialNum == 1)
        {
            _tutorialType = TutorialType.Highlight;
            _Tutorials[1].SetActive(true);

            // Disable all other _Tutorials game objects
            for (int i = 1; i < _Tutorials.Length; i++)
            {
                if (i != 1) // Skip index 1
                {
                    _Tutorials[i].SetActive(false);
                }
            }
        }
        if (_medDialogue.tutorialNum == 2)
        {
            _tutorialType = TutorialType.TensionBar;
            _Tutorials[2].SetActive(true);

            // Disable all other _Tutorials game objects
            for (int i = 1; i < _Tutorials.Length; i++)
            {
                if (i != 2) // Skip index 0
                {
                    _Tutorials[i].SetActive(false);
                }
            }
        }
        if (_medDialogue.tutorialNum == 3)
        {
            _tutorialType = TutorialType.Tells;
            _Tutorials[3].SetActive(true);

            // Disable all other _Tutorials game objects
            for (int i = 1; i < _Tutorials.Length; i++)
            {
                if (i != 3) // Skip index 0
                {
                    _Tutorials[i].SetActive(false);
                }
            }
        }

        if(_medDialogue.tutorialNum == 4)
        {
            _tutorialType = TutorialType.End;
                foreach(GameObject tut in _Tutorials)
            {
                tut.SetActive(false);
            }
        }
    }

    public void DisplayTutorial()
    {

    }
}
