using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialPopup : MonoBehaviour
{

    public List<string> TutorialText;
    public TextMeshProUGUI tutorialText;
    public int tutNumb;
    public CanvasGroup _tutorialGroup;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    }
    public void DisplayTutText()
    {
        tutorialText.text = TutorialText[tutNumb];
    }

    public void DestroyTutText()
    {
        _tutorialGroup.alpha = 0;
    }
}
