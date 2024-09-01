using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class CampDialogue : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;
    public UnityEngine.UI.Slider YSlider;
    public UnityEngine.UI.Slider JSlider;
    public GameObject backgroundCanvas;
    public AudioSource reverseAud;
    public Text speakerName;
    public string[] currentInktags;
    public GameObject[] speakerID;
    public int YML;


    // Define the UI prefab for narrator text (set in Unity editor)
    [SerializeField]
    private Text narratorTextPrefab = null;

    void Awake()
    {
        // Remove the default message
        RemoveChildren();
        StartStory();
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
        YSlider.value = (int)story.variablesState["YMorale"];
        JSlider.value = (int)story.variablesState["JPoints"];
        YML = (int)story.variablesState["YMorale"];

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
        textLayoutGroup.childControlHeight = true;
        textLayoutGroup.childAlignment = TextAnchor.LowerLeft;
        textLayoutGroup.padding.left = -191;
        textLayoutGroup.padding.right = -73;
        textLayoutGroup.padding.top = 217;
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
        choicesLayoutGroup.padding.left = 331;
        choicesLayoutGroup.padding.right = 0;
        choicesLayoutGroup.padding.top = 171;
        choicesLayoutGroup.padding.bottom = 0;
        choicesLayoutGroup.spacing = 0;

        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                CreateChoiceView(choice.text.Trim(), choicesContainer);
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
            Debug.Log("Can continue: " + story.canContinue);
            Debug.Log("Number of choices: " + story.currentChoices.Count);

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


    }



    // Creates a button showing the choice text
    Button CreateChoiceView(string text, GameObject container)
    {
        if (string.IsNullOrEmpty(text))
        {
            return null; // Skip creating a button for empty choices
        }

        Button choice = Instantiate(buttonPrefab, container.transform);
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        if (text == "Back")
        {
            // Handle the "Back" button to close the dialogue
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
                    choice.onClick.AddListener(() => OnClickChoiceButton(choiceToSelect));
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
