using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SnapDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI speakerName;
    public string unitName;
    public float textSpeed;

    // Define your public array of lines
    public string[] linesArray = new string[]
    {
        "Random Line 1",
        "Random Line 2",
        "Random Line 3",
        // Add more lines as needed
    };

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
    }

    void Awake()
    {
        speakerName.text = unitName;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartDialogue();
        }
    }

    public void StartDialogue()
    {
        // Clear the old text
        textComponent.text = string.Empty;

        // Select a random line from the linesArray
        int index = Random.Range(0, linesArray.Length);

        StartCoroutine(TypeLine(linesArray[index]));
    }


    IEnumerator TypeLine(string line)
    {
        foreach (char c in line.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
