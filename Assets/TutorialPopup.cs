using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialPopup : MonoBehaviour
{

    public List<string> TutorialText;
    public TextMeshProUGUI tutorialText;
    public int tutNumb;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayTutText()
    {
        tutorialText.text = TutorialText[tutNumb];
    }
}
