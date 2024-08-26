using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuDropdown : MonoBehaviour
{
    public TextMeshProUGUI output;


    public void HandleInputData(int val)
    {
        if(val == 0)
        {
            Debug.Log("Languid Pace");
            // languid
            output.text = "languid pace text";
        }
        if (val == 1)
        {
            Debug.Log("Moderate Pace");
            // languid
            output.text = "mod pace text";
        }
        if (val == 2)
        {
            // languid
            output.text = "int pace text";
        }
    }
}
