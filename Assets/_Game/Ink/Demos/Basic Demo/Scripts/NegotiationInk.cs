using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class NegotiationInk : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;
    public GameObject backgroundCanvas;
    public UnityEngine.UI.Slider YSlider;
    public UnityEngine.UI.Slider JSlider;
    public BattleSystem bSystem;
    public ListenSystem LSystem;

    void Awake()
    {
        // Remove the default message
        RemoveInkChildren();
        StartInkStory();
        bSystem = GameObject.Find("BATTLE SYSTEM").GetComponent<BattleSystem>();
    }

    // Creates a new Story object with the compiled story which we can then play!
    public void StartInkStory()
    {
        story = new Story(inkJSONAsset.text);
        if (OnCreateStory != null) OnCreateStory(story);
        RefreshView();
        YSlider.value = (int)story.variablesState["YMorale"];
        JSlider.value = (int)story.variablesState["JMorale"];

    }

    // This is the main function called every time the story changes. It does a few things:
    // Destroys all the old content and choices.
    // Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
    void RefreshView()
    {
        // Remove all the UI on screen
        RemoveInkChildren();

        // Create a container for text and buttons
        GameObject contentContainer = new GameObject("ContentContainer");
        contentContainer.transform.SetParent(canvas.transform, false);

        VerticalLayoutGroup verticalLayoutGroup = contentContainer.AddComponent<VerticalLayoutGroup>();
        verticalLayoutGroup.childControlHeight = false;

        // Ensure proper layout attributes for the VerticalLayoutGroup
        verticalLayoutGroup.childForceExpandWidth = false;
        verticalLayoutGroup.childForceExpandHeight = false;
        verticalLayoutGroup.childControlWidth = true;
        verticalLayoutGroup.childControlHeight = true;

        // Set the padding if needed
        verticalLayoutGroup.padding.left = 0;  // Adjust as per your preference
        verticalLayoutGroup.padding.right = 0;
        verticalLayoutGroup.padding.top = 75;
        verticalLayoutGroup.padding.bottom = -75;
        verticalLayoutGroup.spacing = 15; // Adjust this value to set the desired spacing
        verticalLayoutGroup.childAlignment = TextAnchor.LowerCenter; // Adjust as needed



        // Read all the content until we can't continue any more
        while (story.canContinue)
        {
            // Continue gets the next line of the story
            string text = story.ContinueMaximally();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen within the container
            CreateContentView(text, contentContainer);
        }


        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                CreateChoiceView(choice.text.Trim(), contentContainer);
            }
        }
        // If we've read all the content and there are no choices, the story is finished!
        else
        {
                CreateChoiceView("Back", contentContainer);
                
        }
    }
    void Update()
    {
        YSlider.value = (int)story.variablesState["YMorale"];
        if(YSlider.value == YSlider.maxValue)
        {
            Debug.Log("specialUnlock");
            LSystem.YSpecialUnlockButton.interactable = true;
        }
        JSlider.value = (int)story.variablesState["JMorale"];
        if (JSlider.value == JSlider.maxValue)
        {
           Debug.Log("jspecialUnlock");
           LSystem.JSpecialUnlockButton.interactable = true;
       }
    }
    // When we click the choice button, tell the story to choose that choice!
    // When we click the choice button, tell the story to choose that choice
    void OnClickChoiceButton(Choice choice)
    {
        if (choice.text.Trim() == "Back")
        { 
            
            Debug.Log("END PLAYER TURN");
            Debug.Log("deactive selected");
            Deactivate();
           
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
                Debug.Log("Restarting the story.");
                Deactivate();
            }
            else
            {
                Debug.Log("Continuing the story.");
                RefreshView();
            }
        }
    }




    void Deactivate()
    {
        Debug.Log("deactivating...");
        this.gameObject.SetActive(false);
        backgroundCanvas.SetActive(false);
        bSystem.StartJasperTurn();
    }

    // Creates a textbox showing the line of text
    void CreateContentView(string text, GameObject container)
    {
        Text storyText = Instantiate(textPrefab, container.transform);
        storyText.text = text;
    }

    // Creates a button showing the choice text
    // Inside CreateChoiceView function
    // Inside CreateChoiceView function
    // Inside CreateChoiceView function
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
    void RemoveInkChildren()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }
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
