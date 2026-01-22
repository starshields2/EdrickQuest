using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueChoiceManager : MonoBehaviour
{
    public Button[] buttons; // An array to store your UI buttons.
    private int selectedIndex = 0; // The currently selected button index.
    private bool isNavigatingUI = true; // Flag to indicate UI navigation.
    private bool isHorizontalPressed = false; // Flag to track horizontal input.
    private bool isVerticalPressed = false; // Flag to track vertical input.

    private void Start()
    {
        // Set the first button as selected when the script starts.
        SelectButton(selectedIndex);
    }

    private void Update()
    {
        if (isNavigatingUI)
        {
            // Check for user input to navigate between buttons.
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Update the horizontal and vertical input flags.
            isHorizontalPressed = Mathf.Abs(horizontalInput) > 0.1f;
            isVerticalPressed = Mathf.Abs(verticalInput) > 0.1f;

            // Calculate the new selected index based on input.
            int newSelectedIndex = selectedIndex;

            if (isHorizontalPressed)
            {
                // Horizontal input is stronger, use it to navigate left/right.
                newSelectedIndex += (int)Mathf.Sign(horizontalInput);
            }
            else if (isVerticalPressed)
            {
                // Vertical input is stronger, use it to navigate up/down.
                newSelectedIndex += (int)Mathf.Sign(verticalInput);
            }

            // Ensure the newSelectedIndex is within valid bounds.
            newSelectedIndex = Mathf.Clamp(newSelectedIndex, 0, buttons.Length - 1);

            if (newSelectedIndex != selectedIndex)
            {
                // Deselect the currently selected button.
                DeselectButton(selectedIndex);

                // Select the newly calculated button.
                SelectButton(newSelectedIndex);

                // Update the selectedIndex.
                selectedIndex = newSelectedIndex;
            }

            // Check for user input to press the selected button.
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                // Trigger the click event of the selected button.
                buttons[selectedIndex].onClick.Invoke();
            }

            // Check for user input to exit UI navigation mode.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isNavigatingUI = false;
            }
        }
        else
        {
            // Handle character movement or other gameplay input here.

            // Example: Move the character based on WASD keys.
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            // Apply character movement logic here.
        }
    }

    private void SelectButton(int index)
    {
        buttons[index].Select(); // Select the button to highlight it.
        buttons[index].OnSelect(null); // Trigger the OnSelect event (if any).
    }

    private void DeselectButton(int index)
    {
        buttons[index].OnDeselect(null); // Trigger the OnDeselect event (if any).
    }
}
