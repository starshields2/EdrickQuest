using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.Rendering;


public class MediationDialogue : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;
    public static bool IsDialogueActive { get; private set; }

    public enum DialogueType
    {
        None,
        Mediation,
        Event,
        Cutscene
    }

    public DialogueType _dType = DialogueType.Mediation;

    [SerializeField] private bool startAutomatically = true; // Option to start the story immediately

    [Header("Popups")]
    [SerializeField] private float _postProcessBlendWeight = 1;
    [SerializeField] private Volume _postProcess;
    [SerializeField] private GameObject[] _answer;
    [SerializeField] private int _answerIndex;
    public int tutorialNum;
    [SerializeField] private GameObject NotesUpdateIndicator;
    [SerializeField] private GameObject EventResolutionPanel;

    [Header("Dialogue History")]
    public List<string> notesDescriptions; //
                                           // public List<string> notesTitles;
                                           //public Button notesPFButton;
    public GameObject notesContainer;
    public GameObject notesTextPF;
    //public int noteIndex;



    public List<string> dialogueHistory = new List<string>(); //all dialogue to be logged in history
    [SerializeField]
    private GameObject _DialogueHistoryTextPF = null; // Reference to your existing dialogue history prefab
    [SerializeField]
    private GameObject _HistoryContainer = null; // Reference to your existing dialogue history prefab
    public Transform _dHistoryParent; // parent object where the dialogue should be nested under.

    [Header("Story Items")]

    public UnityEngine.UI.Slider _tensDisplay;
    private TensionSlider _tensionSliderScript;

    public GameObject backgroundCanvas; //background
    public AudioSource reverseAud; //audio that plays when mediation begins
    public Text speakerName; //speaker name.
    public string[] currentInktags; //current tags to track
    public GameObject[] speakerID;
    private Color newNormalColor = Color.red;
    public bool pass;
    public int _diceRoll;
    //public SkillMenu skillMenu; (depreciated game object ref for skills)

    [Header("Tension")]
    public TensionCounter _tensMeter; // the tension management script.
    public Slider _tensionSlider; //public slider where this mediation's tension value will be displayed. 
    //public Material tensionMaterial; // <- old material for tension slider
    public OverworldManager _overworldManager; // <- overworld manager for tension and other global variables

    //for calculating tension 
    public int _CurrentTension;

    public bool _highTension;
    public bool _lowTension;
    //public int _flaggedCoreNeed;

    //public float _valuesMult;
    //public float _commonsMult;

    public UIBinder _UIBinder;

    // Define the UI prefab for narrator text
    [SerializeField]
    private Text narratorTextPrefab = null;

    [SerializeField] private TooltipHandler _tooltipHandlerRef;

    public static event Action<int> OnTensionChanged; // Event for tension changes

    private int _previousTension; // To track the previous tension value

    // If a trigger started this story, we store it so we can report results back to it
    private EventTrigger_Overworld _initiatingTrigger = null;

    private bool _isPlaying = false;

    void Start()
    {
        //_UIBinder = GameObject.Find("DataManager").GetComponent<UIBinder>();
        // _UIBinder.GetDialogueInfo();


        // _oldTensionValue = _tensMeter._tension;
        //  Debug.Log(story.currentTags.Length);

        RemoveChildren();

        if (startAutomatically)
        {
            StartStory();
        }
        else
        {
            this.gameObject.SetActive(false);
            if (backgroundCanvas != null)
                backgroundCanvas.SetActive(false);
        }

        _tensionSliderScript = _tensDisplay.GetComponent<TensionSlider>();

        story.variablesState["tension"] = _overworldManager._publicTension;
    }

    //get global tension and set this mediation to that value. 
    private void SetCurrentTension()
    {

    }

    private void SetDialogueType()
    {
        if (story == null)
        {
            // Story is not initialized yet, so dialogue type can't be set
            return;
        }

        int _getInkTypeValue = (int)story.variablesState["type"];

        if (_getInkTypeValue == 0)
        {
            _dType = DialogueType.None;
        }
        if (_getInkTypeValue == 1)
        {
            _dType = DialogueType.Mediation;
        }
        if (_getInkTypeValue == 2)
        {
            _dType = DialogueType.Event;
        }
        if (_getInkTypeValue == 3)
        {
            _dType = DialogueType.Cutscene;
        }
    }

    // Subscribe to the OnTensionChanged event
    void OnEnable()
    {
        OnTensionChanged += HandleTensionChanged;
    }

    private void HandleTensionChanged(int newTension)
    {
        // Update the tension slider value
        _tensionSliderScript.SetTensionValue(newTension);
    }

    void OnDisable()
    {
        OnTensionChanged -= HandleTensionChanged;
    }

    void Awake()
    {
        SetDialogueType(); //gets ink  variable of Type and sets enum.
        StartPostProcess();
    }

    private void StartPostProcess()
    {
        _postProcess.weight = 1;
    }
    public void BindSliders(Slider tension, Slider tp, Companion[] comps = null)
    {
        _tensionSlider = tension;
        //_TPSlider = tp
    }

    // Creates a new Story object with the compiled story which we can then play!
    public void StartStory()
    {
        IsDialogueActive = true;
        story = new Story(inkJSONAsset.text);

        if (OnCreateStory != null) OnCreateStory(story);

        //bind external functions from Ink here:
        story.BindExternalFunction("UpdateNote", () =>
        {
            CreateNotesButton();
        });
        story.BindExternalFunction("ShowObjection", () => {
            Debug.Log("OBJECTION!");
            PopupPortrait();
        });


        story.BindExternalFunction("CalculateEventResults", () => {
            Debug.Log("CALCULATE EVENT RESULT!");
            CalculateEventResult();
        });



        RefreshView();
        DisplayTags();
    }

    //Debug log for tag display
    void DisplayTags()
    {
        foreach (string tag in story.currentTags)
        {
            Debug.Log("Tag: " + tag);
        }
    }


    [ContextMenu("Create New Note")]
    public void CreateNotesButton() //currently just removes top of the list.
    {
        //GameObject notesContainer = GameObject.Find("NotesContainer");
        //Button clone = Instantiate(notesPFButton);
        //clone.transform.SetParent(notesContainer.transform, false);
        //Transform child = clone.transform.GetChild(0);
        //TextMeshProUGUI noteTitleText = child.GetComponent<TextMeshProUGUI>();
        //noteTitleText.text = notesTitles[noteIndex];
        //notesTitles.RemoveAt(noteIndex);
        int noteIndex;
        noteIndex = (int)story.variablesState["NotesIndex"];
        GameObject descContainer = GameObject.Find("NotesDescriptionsContainer");
        GameObject notesClone = Instantiate(notesTextPF);
        notesClone.transform.SetParent(descContainer.transform, false);
        TextMeshProUGUI notesTextComponent = notesClone.GetComponent<TextMeshProUGUI>();
        notesTextComponent.text = notesDescriptions[noteIndex];
        StartCoroutine(UpdateNotesFlash());
        //notesDescriptions.RemoveAt(noteIndex);
    }

    private IEnumerator UpdateNotesFlash()
    {
        Debug.Log("FlashNotes");
        NotesUpdateIndicator.SetActive(true);
        yield return new WaitForSeconds(1);
        NotesUpdateIndicator.SetActive(false);
    }


    void Update()
    {
        if (story == null) return;

        if (story.variablesState["tension"] != null)
        {
            _CurrentTension = (int)story.variablesState["tension"];
        }

        if (story.variablesState["ExternalTutorialNum"] != null)
        {
            tutorialNum = (int)story.variablesState["ExternalTutorialNum"];
        }

        // if(story.variablesState["NotesIndex"] != null)
        // {
        //     noteIndex = (int)story.variablesState["NotesIndex"];
        // }

        if (story.variablesState["PopupNum"] != null)
        {
            _answerIndex = (int)story.variablesState["PopupNum"];
        }

        // Check if tension has changed
        if (_CurrentTension != _previousTension)
        {
            OnTensionChanged?.Invoke(_CurrentTension); // Trigger the event
            _previousTension = _CurrentTension; // Update the previous tension value
        }

        if (_CurrentTension > 14)
        {
            _highTension = true;
            _lowTension = false;


        }
        if (_CurrentTension < 5)
        {
            _lowTension = true;
            _highTension = false;
        }
    }

    //on closing the mediation window + clicking on choices, calculate tension with this: (depreciated lowkey)
    public void CheckNewTensionValue()
    {

    }

    //what happens when choices are clicked, but can be called any time?
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

        // Read all the content until can't continue any more
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
        choicesLayoutGroup.padding.left = 366;
        choicesLayoutGroup.padding.right = 0;
        choicesLayoutGroup.padding.top = -183;
        choicesLayoutGroup.padding.bottom = 0;
        choicesLayoutGroup.spacing = 125;
        //Create container for Edrick's continuing speech.

        GameObject _SPchoicesContainer = new GameObject("SpecialChoicesContainer");
        _SPchoicesContainer.transform.SetParent(canvas.transform, false);


        if (_dType == DialogueType.Cutscene)
        {

            VerticalLayoutGroup _SPchoicesLayoutGroup = _SPchoicesContainer.AddComponent<VerticalLayoutGroup>();
            _SPchoicesLayoutGroup.childControlHeight = false;
            _SPchoicesLayoutGroup.childForceExpandWidth = false;
            _SPchoicesLayoutGroup.childForceExpandHeight = false;
            _SPchoicesLayoutGroup.childControlWidth = false;
            _SPchoicesLayoutGroup.childControlHeight = true;
            _SPchoicesLayoutGroup.childAlignment = TextAnchor.LowerLeft;
            _SPchoicesLayoutGroup.padding.left = 305;
            _SPchoicesLayoutGroup.padding.right = 0;
            _SPchoicesLayoutGroup.padding.top = 392;
            _SPchoicesLayoutGroup.padding.bottom = 0;
            _SPchoicesLayoutGroup.spacing = 125;
        }
        else
        {


            VerticalLayoutGroup _SPchoicesLayoutGroup = _SPchoicesContainer.AddComponent<VerticalLayoutGroup>();
            _SPchoicesLayoutGroup.childControlHeight = false;
            _SPchoicesLayoutGroup.childForceExpandWidth = false;
            _SPchoicesLayoutGroup.childForceExpandHeight = false;
            _SPchoicesLayoutGroup.childControlWidth = false;
            _SPchoicesLayoutGroup.childControlHeight = true;
            _SPchoicesLayoutGroup.childAlignment = TextAnchor.LowerLeft;
            _SPchoicesLayoutGroup.padding.left = -112;
            _SPchoicesLayoutGroup.padding.right = 0;
            _SPchoicesLayoutGroup.padding.top = 420;
            _SPchoicesLayoutGroup.padding.bottom = 0;
            _SPchoicesLayoutGroup.spacing = 125;

        }



        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                TrackChoiceHistory(choice.text);
                foreach (string tag in choice.tags)
                {
                    if (tag == "EdrickContinue")
                    {
                        CreateContinueChoiceView(choice.text.Trim(), _SPchoicesContainer);
                    }
                    if (tag == "EdrickChoice")
                    {
                        CreateChoiceView(choice.text.Trim(), choicesContainer);
                    }
                    if (tag == "NewInfo")
                    {
                        Debug.Log("NEW INFO: " + buttonPrefab.gameObject.name);
                        Button createdButton = CreateChoiceView(choice.text.Trim(), choicesContainer);
                        MediationButtonHandler handler = createdButton.GetComponent<MediationButtonHandler>();
                        Debug.Log("ANIMATION: " + handler);
                        handler.PlayPing();
                    }
                }
            }
        }

        // If we've read all the content and there are no choices, the story is finished!
        else
        {
            // Display the narrator line
            CreateContentView("There's nothing else to say here.", textContainer);

            // Then show the Back button
            CreateChoiceView("Back", _SPchoicesContainer);
        }

        //then, check tension
        CheckNewTensionValue();



    }



    // When we click the choice button, tell the story to choose that choice
    void OnClickChoiceButton(Choice choice)
    {
        if (choice.text.Trim() == "Back" || choice.text.Trim() == "Go forth.")
        {
            //RestartStory();
            
            Deactivate();
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
                //RestartStory();
                Deactivate();

            }
            else
            {

                RefreshView();
            }
        }
    }

    void Deactivate()
    {
        IsDialogueActive = false;
        CheckNewTensionValue();
        Debug.Log("deactivating...");
        this.gameObject.SetActive(false);
        backgroundCanvas.SetActive(false);

        story.UnbindExternalFunction("ShowObjection");
        GetandSetTension();
        //CalculateEventResult();
        _postProcess.weight = 0;
    }

    // Creates a textbox showing the line of text
    void CreateContentView(string text, GameObject container)
    {
        //Display Text and debug who is talking.
        DisplayTags();
        TextMeshProUGUI storyText = Instantiate(textPrefab, container.transform);
        storyText.text = text;
        _tooltipHandlerRef.UpdateText(storyText, storyText.text);

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

        if (story.currentTags.Contains("YaelPensive"))
        {
            speakerName.text = "Yael";
            speakerID[3].SetActive(true);

            // Disable all other speakerID game objects
            for (int i = 1; i < speakerID.Length; i++)
            {
                if (i != 3) // Skip index 3 (YaelTell)
                {
                    speakerID[i].SetActive(false);
                }
            }
        }

        if (story.currentTags.Contains("YaelAngry"))
        {
            speakerName.text = "Yael";
            speakerID[4].SetActive(true);

            // Disable all other speakerID game objects
            for (int i = 1; i < speakerID.Length; i++)
            {
                if (i != 4) // Skip index 3 (YaelTell)
                {
                    speakerID[i].SetActive(false);
                }
            }
        }

        if (story.currentTags.Contains("YaelTell"))
        {
            speakerName.text = "Yael";
            speakerID[5].SetActive(true);

            GetComponent<TellFeedback>().PlayTell();

            // Disable all other speakerID game objects
            for (int i = 1; i < speakerID.Length; i++)
            {
                if (i != 5) // Skip index 3 (YaelTell)
                {
                    speakerID[i].SetActive(false);
                }
            }
        }

        TrackDialogueHistory(text);
        DisplayDialogueHistory();
    }

    private void TrackDialogueHistory(string line)
    {
        string currentSpeaker = speakerName != null ? speakerName.text : "Unknown";
 
            dialogueHistory.Add(currentSpeaker + ": " + line);
        
    }

    private void TrackChoiceHistory(string line)
    {
        //dialogueHistory.Add(line);   
    }

    private void ClearDialogueHistory()
    {
        dialogueHistory.Clear();
    }

    [ContextMenu("DisplayHistory")]
    public void DisplayDialogueHistory()
    {
        GameObject historyContainer = GameObject.Find("Viewport");

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

        ClearDialogueHistory();
    }

    [ContextMenu("Take Notes")]
    public void TrackNotes()
    {

    }

    public void CheckTellStrength() //when clicking on a tell, check if tension is low enough to warrant providing new information.
    {
        if(_lowTension == true)
        {
            Debug.Log("Tension Low, Proceed");
            //call yaeltell true
            //swap to pensive sprite
            var result = story.EvaluateFunction("YaelTellTrue", 2);
            Debug.Log("Result from Ink: " + result);
        }
        else if(_highTension== true)
        {
            Debug.Log("Tension too High, Do not Proceed");
            //call yaeltell false
            var result = story.EvaluateFunction("YaelTellFalse", 1);
            Debug.Log("Result from Ink: " + result);  

            //swap to angy sprite
            //continue story

            
        }
    }
    //create continue button
    Button CreateContinueChoiceView(string text, GameObject container)
    {
        if (string.IsNullOrEmpty(text))
        {
            return null; // Skip empty choices
        }


        Button choice = Instantiate(continueButtonPrefab, container.transform);
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        if (text == "Back")
        {
            choice.onClick.AddListener(() => Deactivate());
            // RestartStory();
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
                        TrackDialogueHistory("Edrick: " + text);

                        // Continue with the selected choice
                        OnClickChoiceButton(choiceToSelect);
                    });
                }
            }
        }

        return choice;
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
            // RestartStory();
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

    //for the popups on correct answer choices, objections, affirmations, etc.
    [ContextMenu("PopupPortrait")]
    public void PopupPortrait()
    {
        StartCoroutine(PopupPortraitLogic());
    }

    private IEnumerator PopupPortraitLogic()
    {
        int currentAnswerIndex;
        currentAnswerIndex = (int)story.variablesState["PopupNum"];
        _answer[currentAnswerIndex].SetActive(true);
        yield return new WaitForSeconds(2);
        _answer[currentAnswerIndex].SetActive(false);
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
    // Accept an optional initiating trigger so we can notify it of event results
    public void SetNewStory(TextAsset newJSON, EventTrigger_Overworld initiator = null)
    {
        this.inkJSONAsset = newJSON;
        _initiatingTrigger = initiator;
    }



    //All this does is take the ink tension and set it to a public value that can be called elsewhere?
    public void GetandSetTension()
    {
        _overworldManager._publicTension = _CurrentTension;
    }

    //If it's an event dialogue, calculate success! 
    public void CalculateEventResult()
    {
        int GetDialogueType;
        GetDialogueType = (int)story.variablesState["type"];

        if(GetDialogueType == 1)
        {
            //Display the Event Summary for Mediation
        }

        if(GetDialogueType == 2)
        {
            //bool pass;
            bool fail;
            bool critpass;
            bool critfail;
             _diceRoll = UnityEngine.Random.Range(1, 20);

            Debug.Log(_diceRoll);
            if(_diceRoll >= _overworldManager._difficultyCheck)
            {
                pass = true;
                Debug.Log("passed difficulty check.");
            }
            else
            {
                Debug.Log("failed difficulty check.");
            }
            story.variablesState["success"] = pass;
            GameObject EventSummary = Instantiate(EventResolutionPanel, this.gameObject.transform);

            // Notify the initiating trigger (if any) whether the event succeeded
            if (_initiatingTrigger != null)
            {
                _initiatingTrigger.OnEventSuccess?.Invoke(pass);
                _initiatingTrigger = null;
            }
        }

        if(GetDialogueType == 3)
        {
            Debug.Log("Activating Cutscene");
            //Cutscene Event
            IntroductionManagement intro = GameObject.Find("IntroductionManagement").GetComponent<IntroductionManagement>();
            intro.StartOpeningCutscene();
           
        }
    }

    [SerializeField] private TextAsset inkJSONAsset = null;
    public Story story;

    [SerializeField]
    private Canvas canvas = null;

    // UI Prefabs
    [SerializeField]
    private TextMeshProUGUI textPrefab = null;
    [SerializeField]
    private Button buttonPrefab = null;
    [SerializeField]
    private Button continueButtonPrefab = null;
}
