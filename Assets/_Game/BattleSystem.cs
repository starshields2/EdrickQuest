using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSystem : MonoBehaviour
{   
    
    public bool isTurn;
    public bool isEdTurn;
    //public TextMeshProUGUI enemyText;
    public bool isTiming;

    [Header("Units")]
    public Unit playerUnit;
    public Unit enemyUnit;
    public EnemyBattleManager manager;

    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject jasperPrefab;
    public GameObject yaelPrefab;

    [Header("Transforms")]
    public Transform playerBS;
    public Transform enemyBS;
    public Transform JasperBS;
    public Transform YaelBS;

    [Header("HUD Stuff")]
    public GameObject UICANVAS;
    //public BattleHUD playerHUD;
    //public BattleHUD enemyHUD;
    public Slider turnTime;
    public Image slider1Fill;

    [Header("Ink Components")]
    public List<GameObject> JasperInk;
    public List<GameObject> YaelInk;
    public GameObject EdrickDoneConvo;



    public enum BattleState
    {
        Idle,
        Start,
        PlayerTurn,
        JasperTurn,
        YaelTurn,
        EnemyTurn,
        Won,
        Lost
    }

    public BattleState state;
    private float originalTimeScale; // Store the original time scale
    public float playerTurnTimer = 15f; // Timer for player's turn
    public GameObject playerBattleOptions;
    public GameObject jasperBattleOptions;
    public GameObject yaelBattleOptions;
    

    // Start is called before the first frame update
    void Start()
    {
        isTiming = true;
        state = BattleState.Start;
        originalTimeScale = Time.timeScale; // Store the original time scale
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case BattleState.Start:
                UICANVAS.SetActive(true);
                
                //state = BattleState.PlayerTurn;
                break;


                // -------------- EDRICK'S TURN --------------------------
            case BattleState.PlayerTurn:
                UICANVAS.SetActive(false);
                isTurn = true;
                isEdTurn = true;
                if (isTiming)
                {
                    playerBattleOptions.SetActive(true);
                slider1Fill.color = Color.red;
                turnTime.value = playerTurnTimer;
               // Time.timeScale = originalTimeScale;
                // Reduce the player turn timer
                playerTurnTimer -= Time.deltaTime;
                

                if (playerTurnTimer <= 0f)
                {
                    playerBattleOptions.SetActive(false);
                     state = BattleState.JasperTurn;
                    playerTurnTimer = 15f; 
                }

                }

                if (!isTiming)
                {
                    playerTurnTimer = 15f;
                }
               
               

                break;

                /////------------------ JASPER'S TURN -----------------------
                ///
            case BattleState.JasperTurn:
                isTurn = true;
                isEdTurn = false;
                isTiming = true;
                if (isTiming)
                {
                jasperBattleOptions.SetActive(true);
                //SLIDER STUFF.
                slider1Fill.color = Color.yellow;
                turnTime.value = playerTurnTimer;
                playerTurnTimer -= Time.deltaTime; 
                }
               


                if (playerTurnTimer <= 0f)
                {
                    jasperBattleOptions.SetActive(false);
                    state = BattleState.YaelTurn;
                    playerTurnTimer = 15f;
                }
                if (!isTiming)
                {
                    playerTurnTimer = 15f;
                }

                break;
                //-------- YAEL'S TURN-----------------
            case BattleState.YaelTurn:
                isTurn = true;
                isEdTurn = false;
                if (isTiming)
                {
                yaelBattleOptions.SetActive(true);
              
                turnTime.value = playerTurnTimer;
                playerTurnTimer -= Time.deltaTime;
                slider1Fill.color = Color.blue;
                }
                
                
                if (playerTurnTimer <= 0f)
                {
                   yaelBattleOptions.SetActive(false);
                    state = BattleState.EnemyTurn;
                    playerTurnTimer = 15f;
                }

                if (!isTiming)
                {
                    playerTurnTimer = 15f;
                }
                break;
                /////----------- ENEMY TURN ---------------
            case BattleState.EnemyTurn:
                isTurn = false;
                isEdTurn = false;
                Debug.Log("ENEMY TURN");
                Time.timeScale = originalTimeScale;

                EnemyAttack();



                state = BattleState.PlayerTurn; // After enemy's turn, switch back to player's turn
                break;

            case BattleState.Won:
                Debug.Log("You win");
            break;

        }
    }
    [ContextMenu("SetUp")]
    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBS);
        playerUnit = playerGO.GetComponent<Unit>();
        
        GameObject JasperGO = Instantiate(jasperPrefab, JasperBS);
        playerUnit = JasperGO.GetComponent<Unit>();
        
        GameObject YaelGO = Instantiate(yaelPrefab, YaelBS);
        playerUnit = YaelGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBS);
        enemyUnit = enemyGO.GetComponent<Unit>();

      //  playerHUD.SetHUD(playerUnit);
      //  enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PlayerTurn;
        StartPlayerTurn();
    }

    // Call this method to start the player's turn
    public void StartBattle()
    {
        state = BattleState.Start;
    }

    public void StartPlayerTurn()
    {
        state = BattleState.PlayerTurn;
        //feedback


    }
    // Call this method to start the enemy's turn
    public void StartEnemyTurn()
    {
      
        state = BattleState.EnemyTurn;
        isTiming = true;
    }

    public void StartJasperTurn()
    {
        state = BattleState.JasperTurn;

    }

    public void StartYaelTurn()
    {
        state = BattleState.YaelTurn;
    }

    // Call this method to reset the time scale and state when the battle ends
    public void EndBattle()
    {
        
        state = BattleState.Start;
    }

    public void PauseTimer()
    {
        isTiming = false;
    }

    //------ATTACKS.
    public void JasperAttack()
    {
        EnemyBattleManager manager = GetComponent<EnemyBattleManager>();

            Unit chosenEnemy = manager.enemies[manager.currentEnemyIndex].GetComponent<Unit>();
            Debug.Log("Jas attack");
            chosenEnemy.TakeDamage(1);
            Debug.Log("1 damage to " + chosenEnemy.name);
        
      
          //Debug.LogError("Invalid currentEnemyIndex: " + manager.currentEnemyIndex);
        
    }

    public void JasperSpecial()
    {
        EnemyBattleManager manager = GetComponent<EnemyBattleManager>();
        Unit Yael = manager.allies[0].GetComponent<Unit>();
        Yael.Protected();
    }

    public void EdrickTalk()
    {
        EnemyBattleManager manager = GetComponent<EnemyBattleManager>();
        Unit chosenAlly = manager.allies[manager.currentAllyIndex].GetComponent<Unit>();
        Debug.Log("Edrick has started negotiation");

        if (manager.currentAllyIndex == 0)
        {
            int randomIndex = Random.Range(0, YaelInk.Count);
            GameObject selectedInk = YaelInk[randomIndex];
            selectedInk.SetActive(true);
            YaelInk.RemoveAt(randomIndex);

          
        }

        if (manager.currentAllyIndex == 1)
        {
            int randomIndex = Random.Range(0, JasperInk.Count);
            GameObject selectedInk = JasperInk[randomIndex];
            selectedInk.SetActive(true);
            JasperInk.RemoveAt(randomIndex);
        }
        if (JasperInk.Count == 0)
        {
            EdrickDoneConvo.SetActive(true);
        }
        if (YaelInk.Count == 0)
        {
            EdrickDoneConvo.SetActive(true);
        }
    }

    public void YaelHeal()
    {
        EnemyBattleManager manager = GetComponent<EnemyBattleManager>();
        Unit Jasper = manager.allies[0].GetComponent<Unit>();
        Jasper.Heal();
    }
    public void YaelDefend()
    {
        GameObject Yael = GameObject.Find("Yael_BASE");
        Unit YaelUN = Yael.GetComponent<Unit>();
        YaelUN.Defend();
    }

    public void YaelAttack()
    {
        Debug.Log("Debuffing...");
        EnemyBattleManager manager = GetComponent<EnemyBattleManager>();
        Unit chosenEnemy = manager.enemies[manager.currentEnemyIndex].GetComponent<Unit>();

        // Provide a specific damage value when calling DamageDebuff
        chosenEnemy.DamageDebuff(1); // Replace 10 with the desired damage value
        Debug.Log("Damage debuf");
    }


    IEnumerator Berate()
    {
        yield return new WaitForSeconds(1f);
        state = BattleState.EnemyTurn;
    }
 
    public void EnemyAttack()
    {
        manager.SelectAlly();
        Unit selectedUnit = manager.selectedAlly.GetComponent<Unit>();
        selectedUnit.TakeDamage(1); // Replace 10 with the appropriate damage value

    }



}
