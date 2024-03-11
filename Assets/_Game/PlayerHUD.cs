using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public Unit playerUnit;
    private Slider playerHealth;
    public GameObject playerCanvas;

    // Start is called before the first frame update
    void Awake()
    {
        playerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
        playerHealth = playerUnit.health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPTurn()
    {
        playerCanvas.SetActive(true);
    }
}
