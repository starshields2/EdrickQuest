using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkillTree : MonoBehaviour
{
    public GameObject[] Skills;
    public List<string> SkillInfo;
    public List<string> EdQuote;
    public List<string> YaQuote;
    public bool skillAvailable = false;
    public GameObject skillBox;
    public TextMeshProUGUI YQuote;
    public TextMeshProUGUI EQuote;
    public TextMeshProUGUI skillDescription;

    public bool BerateOn = false;
    public bool HealOn = false;
    public Button skill1;
    public Button skill2;

    private RectTransform skillBoxRectTransform;

    void Start()
    {
        skillBoxRectTransform = skillBox.GetComponent<RectTransform>();

        // Add EventTrigger component to each skill object
        foreach (GameObject skill in Skills)
        {
            EventTrigger eventTrigger = skill.AddComponent<EventTrigger>();

            // Add PointerEnter and PointerExit events
            EventTrigger.Entry entryEnter = new EventTrigger.Entry();
            entryEnter.eventID = EventTriggerType.PointerEnter;
            entryEnter.callback.AddListener((data) => { OnSkillHoverEnter((PointerEventData)data); });
            eventTrigger.triggers.Add(entryEnter);

            EventTrigger.Entry entryExit = new EventTrigger.Entry();
            entryExit.eventID = EventTriggerType.PointerExit;
            entryExit.callback.AddListener((data) => { OnSkillHoverExit(); });
            eventTrigger.triggers.Add(entryExit);
        }
    }

    private bool healOnCalled = false;

    public void SetHealOn()
    {
        skill1.interactable = true;
        BerateOn = true;

    }

    public void SetBerateOn()
            {
                skill2.interactable = true;
            }

    public void OnSkillHoverEnter(PointerEventData eventData)
    {
        Debug.Log("OnSkillHoverEnter called!");

        GameObject hoveredSkill = eventData.pointerEnter;

        // Find the index of the hovered skill in the Skills array
        int skillIndex = System.Array.IndexOf(Skills, hoveredSkill);

        Debug.Log("Hovered Skill Index: " + skillIndex);

        if (skillIndex >= 0 && skillIndex < SkillInfo.Count)
        {
            // Update text based on the selected skill
            EQuote.text = EdQuote[skillIndex];
            YQuote.text = YaQuote[skillIndex];
            skillDescription.text = SkillInfo[skillIndex];

            // Position the skillBox next to the hovered skill
            PositionSkillBox(hoveredSkill);

            // Enable the skillBox when the mouse hovers over
            skillBox.SetActive(true);
        }
    }

    public void OnSkillHoverExit()
    {
        Debug.Log("Skill Exit");
        // Disable the skillBox when the mouse exits
        skillBox.SetActive(false);
    }

    private void PositionSkillBox(GameObject hoveredSkill)
    {
        // Get the RectTransform of the hovered skill
        RectTransform hoveredSkillRectTransform = hoveredSkill.GetComponent<RectTransform>();

        // Calculate the position for the skillBox (adjust as needed)
        Vector3 skillBoxPosition = new Vector3(
            hoveredSkillRectTransform.position.x + hoveredSkillRectTransform.rect.width,
            hoveredSkillRectTransform.position.y,
            hoveredSkillRectTransform.position.z
        );

        // Set the position of the skillBox
        skillBoxRectTransform.position = skillBoxPosition;
    }
}
