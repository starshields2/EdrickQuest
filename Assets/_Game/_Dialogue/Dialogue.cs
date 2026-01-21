using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI speakerName;
    public bool textStart;
    public string unitName;
    public string[] lines;
    public float textSpeed;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
       
    }

    void Awake()
    {
        speakerName.text = unitName;
        NextLine();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }
    [ContextMenu("Dialogue")]
    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        if (!textStart)
        {
            textStart = true;
    foreach (char c in lines[index].ToCharArray())
        {
            yield return new WaitForSeconds(textSpeed);
            textComponent.text += c;  
        }
        }
    
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            StopAllCoroutines();
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
