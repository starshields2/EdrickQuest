using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyBattleManager : MonoBehaviour
{
    public BattleSystem bSystem;
    public GameObject[] allies;
    public int currentAllyIndex = 0;
    public GameObject selectedAlly;

    public GameObject[] enemies;
    public int currentEnemyIndex = 0;
    public RectTransform pointer;
    public float hoverHeight = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        bSystem = GameObject.FindGameObjectWithTag("BSystem").GetComponent<BattleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        allies = GameObject.FindGameObjectsWithTag("Ally");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (bSystem.isTurn)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                SelectNextEnemy();
            }
            if (Input.GetMouseButtonDown(0) || (Input.GetKeyDown(KeyCode.Space)))
            {
                SelectHoveredEnemy();
            }
        }
        if (bSystem.isEdTurn)
        {

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                PlayerSelectNextAlly();
            }
            if (Input.GetMouseButtonDown(0) || (Input.GetKeyDown(KeyCode.Space)))
            {
                //SelectHoveredEnemy();
            }
        }
        if(allies.Length == 0)
        {
            Debug.Log("Failure");
            bSystem.state = BattleSystem.BattleState.Lost;
        }

        if (enemies.Length == 0)
        {
            Debug.Log("Victory!");
            bSystem.state = BattleSystem.BattleState.Won;


        }
    }

    void SelectNextEnemy()
    {
        // Increment the index to select the next enemy
        currentEnemyIndex++;

        // If we reach the end of the array, loop back to the first enemy
        if (currentEnemyIndex >= enemies.Length)
        {
            currentEnemyIndex = 0;
        }

        // Get the world position of the currently selected enemy
        Vector3 targetPosition = enemies[currentEnemyIndex].transform.position;

        // Add the Y offset to position the pointer above the enemy
        targetPosition.y += hoverHeight;

        // Move the pointer to the position with Y offset
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetPosition);
        pointer.position = screenPosition;
    }

    public void SelectAlly()
    {
        if (allies.Length > 0)
        {
            // Generate a random index within the allies array length
            currentAllyIndex = Random.Range(0, allies.Length);

            // Access the randomly selected ally
             selectedAlly = allies[currentAllyIndex];

            // You can now perform actions on the selected ally
            Debug.Log("Selected Ally: " + selectedAlly.name);
            // Add your action code here
        }
        else
        {
            Debug.Log("No allies found.");
        }
    }

    public void PlayerSelectNextAlly()
    {
        // Increment the index to select the next enemy
        currentAllyIndex++;

        // If we reach the end of the array, loop back to the first enemy
        if (currentAllyIndex >= allies.Length)
        {
            currentAllyIndex = 0;
        }

        // Get the world position of the currently selected enemy
        Vector3 targetPosition = enemies[currentAllyIndex].transform.position;

        // Add the Y offset to position the pointer above the enemy
        targetPosition.y += hoverHeight;

        // Move the pointer to the position with Y offset
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetPosition);
        pointer.position = screenPosition;
    }



    void SelectHoveredEnemy()
    {
        Debug.Log("Click");
        // Cast a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object has the "Enemy" tag
            if (hit.collider.CompareTag("Enemy"))
            {
                // Perform your action on the selected enemy
               // Debug.Log("Selected Enemy: " + hit.collider.gameObject.name);
                // Add your action code here
            }
            else
            {
                //Debug.Log("Hit something, but not an enemy.");
            }
        }
        else
        {
           // Debug.Log("Nothing hit.");
        }
    }

}
