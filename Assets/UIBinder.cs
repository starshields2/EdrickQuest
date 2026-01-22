using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBinder : MonoBehaviour
{
    public Slider tensionSlider;
    public Slider tpSlider;

    public Companion[] companions;

    void Start()
    {
        TensionCounter tc = FindObjectOfType<TensionCounter>();
        if (tc != null)
        {
            tc.BindSliders(tensionSlider, tpSlider, companions);
        }

        // Assign to the class-level field
        companions = GetComponents<Companion>();
    }

    public void GetDialogueInfo()
    {
        CampDialogue cd = FindObjectOfType<CampDialogue>();
            if(cd != null)
        {
            cd.BindSliders(tensionSlider, tpSlider);
        }
    }
}
