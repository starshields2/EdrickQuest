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
        _tutorialGroup.alpha = 1;
        DisplayTutText();
        yield return new WaitForSeconds(5f);
        _tutorialGroup.alpha = 0;
    }
    public void DisplayTutText()
    {
        tutorialText.text = TutorialText[tutNumb];
    }
}
