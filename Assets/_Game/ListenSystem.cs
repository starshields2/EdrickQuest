using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListenSystem : MonoBehaviour
{
    public BattleSystem bSystem;
    //public bool JSpecial;
    //public bool YSpecial;

    //public int currentJSMorale;
    //public int neededJSMorale;

    //public int currentYLMorale;
    //public int neededYLMorale;

    //public Slider JSLIDER;
    //public Slider YSLIDER;

    public Button JSpecialUnlockButton;
    public Button YSpecialUnlockButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void JSpecialUnlock()
    {
        //logic
        Debug.Log("function for special unlock");
        JSpecialUnlockButton.interactable = true;
    }

    public void YSpecialUnlock()
    {
        YSpecialUnlockButton.interactable = true; 
    }

   

}
