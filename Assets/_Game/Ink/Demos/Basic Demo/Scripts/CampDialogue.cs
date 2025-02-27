using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;


public class CampDialogue : MonoBehaviour
{
    
    private List<string> dialogueHistory = new List<string>();
    [SerializeField]
    private GameObject _DialogueHistoryTextPF = null; // Reference to your existing dialogue history prefab
    [SerializeField]
    private GameObject _HistoryContainer = null; // Reference to your existing dialogue history prefab
    public Transform _dHistoryParent;
   

    public static event Action<Story> OnCreateStory;
    public UnityEngine.UI.Slider YSlider;
    public UnityEngine.UI.Slider JSlider;
    public GameObject backgroundCanvas;
    public AudioSource reverseAud;
    public Text speakerName;
    public string[] currentInktags;
    public GameObject[] speakerID;
    public int YML;
    public SkillMenu skillMenu;
    public TensionCounter _tensMeter;
    public int _medPoints;


    // Define the UI prefab for narrator text (set in Unity editor)
    [SerializeField]
    private Text narratorTextPrefab = null;

    private 
    void Awake()
    {
        // Remove the default message
        RemoveChildren();
        StartStory();
        skillMenu = GameObject.Find("SkillManager").GetComponent<SkillMenu>();
        _tensMeter = GameObject.Find("TensionHolder").GetComponent<TensionCounter>();
      //  Debug.Log(story.currentTags.Length);
    }

    // Creates a new Story object with the compiled story which we can then play!
    public void StartStory()
    {
        story = new Story(inkJSONAsset.text);
       
        if (OnCreateStory != null) OnCreateStory(story);
        YSlider.value = (int)story.variablesState["YMorale"];
        JSlider.value = (int)story.variablesState["JPoints"];
        RefreshView();
        DisplayTags();
    }

    // This is the main function called every time the story changes. It does a few things:
    // Destroys all the old content and choices.
    // Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
    void DisplayTags()
    {
        foreach (string tag in story.currentTags)
        {
            Debug.Log("Tag: " + tag);
        }
       
    }

 

    void Update()
    {
        skillMenu._skillPoints = Convert.ToInt32(YSlider.value);
        skillMenu._skillPointsAVO = (int)story.variablesState["avoidanceSP"];
        skillMenu._skillPointsAC = (int)story.variablesState["acommoSP"];
        skillMenu._skillPointsCOMPET = (int)story.variablesState["comproSP"];
        skillMenu._skillPointsAC = (int)story.variablesState["acommoSP"];
        YSlider.value = Convert.ToInt32(story.variablesState["YMorale"]);
        JSlider.value = (int)story.variablesState["JPoints"];
        YML = (int)story.variablesState["YMorale"];
        _medPoints = (int)story.variablesState["medPoints"];


    }
    [ContextMenu("Collect SK")]
    public void CollectPoints()
    {
        skillMenu._skillPoints = Convert.ToInt32(YSlider.value);
        skillMenu._skillPointsAVO = (int)story.variablesState["avoidanceSP"];
        skillMenu._skillPointsAC = (int)story.variablesState["acommoSP"];
        skillMenu._skillPointsCOMPET = (int)story.variablesState["comproSP"];
    }

    public void CollectMedPoints()
    {
        _tensMeter._newInfluence = _medPoints;
        _tensMeter.UpdateInfluence();
    }

    void RefreshView()
    {
        // Remove all the UI on screen
        RemoveChildren();

        // Create a container for text
        GameObject textContainer = new GameObject("TextContainer");
        textContainer.transform.SetParent(canvas.transform, false);

        VerticalLayoutGroup textLayoutGroup = textContainer.AddComponent<VerticalLayoutGroup>();
        textLayoutGroup.childControlHeight = false;
        textLayoutGroup.childForceExpandWidth = false;
        textLayoutGroup.childForceExpandHeight = false;
        textLayoutGroup.childControlWidth = false;
        textLayoutGroup.childControlHeight = false;
        textLayoutGroup.childAlignment = TextAnchor.LowerLeft;
        textLayoutGroup.padding.left = -540;
        textLayoutGroup.padding.right = 0;
        textLayoutGroup.padding.top = 112;
        textLayoutGroup.padding.bottom = 130;
        textLayoutGroup.spacing = 15;

        // Read all the content until we can't continue any more
        while (story.canContinue)
        {
            // Continue gets the next line of the story
            string text = story.ContinueMaximally();

            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen within the text container
            CreateContentView(text, textContainer);
        }

        // Create a container for choices
        GameObject choicesContainer = new GameObject("ChoicesContainer");
        choicesContainer.transform.SetParent(canvas.transform, false);

        VerticalLayoutGroup choicesLayoutGroup = choicesContainer.AddComponent<VerticalLayoutGroup>();
        choicesLayoutGroup.childControlHeight = false;
        choicesLayoutGroup.childForceExpandWidth = false;
        choicesLayoutGroup.childForceExpandHeight = false;
        choicesLayoutGroup.childControlWidth = false;
        choicesLayoutGroup.childControlHeight = true;
        choicesLayoutGroup.childAlignment = TextAnchor.LowerLeft;
        choicesLayoutGroup.padding.left = 26;
        choicesLayoutGroup.padding.right = 0;
        choicesLayoutGroup.padding.top = 57;
        choicesLayoutGroup.padding.bottom = 0;
        choicesLayoutGroup.spacing = 50;

        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                CreateChoiceView(choice.text.Trim(), choicesContainer);
                TrackChoiceHistory(choice.text);
            }
        }
        // If we've read all the content and there are no choices, the story is finished!
        else
        {
            CreateChoiceView("Back", choicesContainer);
        }
    }



    // When we click the choice button, tell the story to choose that choice
    void OnClickChoiceButton(Choice choice)
    {
        if (choice.text.Trim() == "Back")
        {
            RestartStory();
           // Deactivate();
        }
        else
        {
            story.ChooseChoiceIndex(choice.index);

            // Debug log to check values
            //Debug.Log("Can continue: " + story.canContinue);
            //Debug.Log("Number of choices: " + story.currentChoices.Count);

            // Check if the story is complete, and if so, restart it
            if (!story.canContinue && story.currentChoices.Count <= 0)
            {
                reverseAud.Play();
                Debug.Log("Restarting the story.");
                RestartStory();
              //  Deactivate();

            }
            else
            {
               
                RefreshView();
            }
        }
    }

    void Deactivate()
    {
        CollectPoints();
        CollectMedPoints();
        Debug.Log("deactivating...");
        this.gameObject.SetActive(false);
        backgroundCanvas.SetActive(false);
        
    }

    // Creates a textbox showing the line of text
    void CreateContentView(string text, GameObject container)
    {
        //Display Text and debug who is talking.
        DisplayTags();
        Text storyText = Instantiate(textPrefab, container.transform);
        storyText.text = text;

        //dialogeTRACKING
      

        // Check for character tag.
        if (story.currentTags.Contains("Edrick"))
        {
            speakerName.text = "Edrick";
            speakerID[0].SetActive(true);

            // Disable all other speakerID game objects
            for (int i = 1; i < speakerID.Length; i++)
            {
                speakerID[i].SetActive(false);
            }
        }

        if (story.currentTags.Contains("Jasper"))
        {
            speakerName.text = "Jasper";
            speakerID[1].SetActive(true);

            // Disable all other speakerID game objects
            for (int i = 0; i < speakerID.Length; i++)
            {
                if (i != 1) // Skip index 1 (Jasper)
                {
                    speakerID[i].SetActive(false);
                }
            }
        }

        if (story.currentTags.Contains("Yael"))
        {
            speakerName.text = "Yael";
            speakerID[2].SetActive(true);

            // Disable all other speakerID game objects
            for (int i = 0; i < speakerID.Length; i++)
            {
                if (i != 2) // Skip index 2 (Yael)
                {
                    speakerID[i].SetActive(false);
                }
            }
        }

        TrackDialogueHistory(text);
    }

    private void TrackDialogueHistory(string line)
    {
        string currentSpeaker = speakerName != null ? speakerName.text : "Unknown";
        dialogueHistory.Add(currentSpeaker + ": " + line);
    }

    private void TrackChoiceHistory(string line)
    {
        dialogueHistory.Add(line);
    }

    [ContextMenu("DisplayHistory")]
    public void DisplayDialogueHistory()
    {
        GameObject historyContainer = GameObject.Find("Content");

        // Ensure correct parent
        historyContainer.transform.SetParent(_dHistoryParent, false);

        // Display each stored dialogue entry
        foreach (string entry in dialogueHistory)
        {
            GameObject historyTextObject = Instantiate(_DialogueHistoryTextPF, historyContainer.transform);
            TextMeshProUGUI historyText = historyTextObject.GetComponent<TextMeshProUGUI>();

            // Set the text to the preformatted speaker + dialogue
            historyText.text = entry;
        }
    }





    // Creates a button showing the choice text
    Button CreateChoiceView(string text, GameObject container)
    {
        if (string.IsNullOrEmpty(text))
        {
            return null; // Skip empty choices
        }

        Button choice = Instantiate(buttonPrefab, container.transform);
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        if (text == "Back")
        {
            choice.onClick.AddListener(() => Deactivate());
            RestartStory();
        }
        else
        {
            if (story != null && story.currentChoices != null)
            {
                Choice choiceToSelect = story.currentChoices.Find(c => c.text.Trim() == text);
                if (choiceToSelect != null)
                {
                    choice.onClick.AddListener(() =>
                    {
                        // Track the choice in dialogue history
                        TrackDialogueHistory("[You said]: " + text);

                        // Continue with the selected choice
                        OnClickChoiceButton(choiceToSelect);
                    });
                }
            }
        }

        return choice;
    }


    // Destroys all the children of this game object (all the UI)
    void RemoveChildren()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }

    public void RestartStory()
    {
        // Reset the state of the story
        story.ResetState();

        // Restart the story from the beginning
       // story.ResetErrors();
        story.ChoosePathString("START");

        // Call a function to display the first content
        //DisplayNextLine();
    }

    [SerializeField]
    private TextAsset inkJSONAsset = null;
    public Story story;

    [SerializeField]
    private Canvas canvas = null;

    // UI Prefabs
    [SerializeField]
    private Text textPrefab = null;
    [SerializeField]
    private Button buttonPrefab = null;
}
