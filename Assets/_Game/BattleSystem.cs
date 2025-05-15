using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    [Header("Communications")]
    public EnemyBattleManager _enemyManager;
    public TensionCounter _tensMeter;
    public Companion[] companions;
    public RectTransform _pointer;
    public float _hoverHeight = 2.0f;

    [Header("TurnOrder")]
    public List<Unit> Order = new List<Unit>();
    [Header("TurnOrderIcons")]
    public GameObject[] Icons;
    public List<GameObject> IconsList = new List<GameObject>();
    public Transform TurnOrderHolder;
    public int RoundNum;
    public string roundString;
    public TextMeshProUGUI roundText;
    public bool hasFilledRoster;

    [Header("Units")]
    public GameObject[] BattleUnits;
    public TextMeshProUGUI _playerName;

    public Unit playerUnit;
    public Unit enemyUnit;
    public Unit YaelUnit;
    public Unit JasperUnit;

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
    public GameObject BattleCanvas;
    public GameObject PauseMenu;
    public GameObject winCanvas;
    public GameObject loseCanvas;

    [Header("OPTIONS")]
    public GameObject playerBattleOptions;
    public GameObject jasperBattleOptions;
    public GameObject yaelBattleOptions;
    public Transform _selectionArrow;

    [Header("BOOLS")]
    public bool roundStarted;
    public bool isPlayerTurn;
    public bool isJasperTurn;
    public bool isYaelTurn;
    public bool isEnemyTurn;

    public enum BattleState
    {
        Idle,
        Start,
        GetParticipants,
        RoundStart,
        PlayerTurn,
        JasperTurn,
        YaelTurn,
        EnemyTurn,
        RoundEnd,
        RoundBuffer,
        Won,
        Lost
    }

    void Start()
    {
        state = BattleState.Start;
    }

    void Update()
    {
       

        switch (state)
        {
            case BattleState.Start:
                BattleCanvas.SetActive(true); //Open Battle Canvas

                 Icons = GameObject.FindGameObjectsWithTag("TurnIcon"); // get turn icons and add them to the list
                 IconsList.AddRange(Icons);

                foreach (GameObject _icon in Icons)
                {
                    _icon.transform.SetParent(TurnOrderHolder, false); //for each turn icon, make it a child of the holder
                }

                state = BattleState.GetParticipants; //then, move on to get participants
                break;

            case BattleState.GetParticipants: //get all active participants

                if (!hasFilledRoster) //but only if the roster isn't already filled: 
                {

                    FillTurnRoster(); //see roster logic 

                  
                }
                
                if(hasFilledRoster = true)
                {
                    state = BattleState.RoundStart; //start the next round
                }
                break;


            case BattleState.RoundStart: //sets up each round
                roundStarted = false;
                if (!roundStarted) //but only if the round hasn't already started.
                {
                    hasFilledRoster = false;
                    roundStarted = true;
                    RoundNum = RoundNum + 1; //start the round and update the round number card
                    roundString = "ROUND: " + RoundNum.ToString();
                    roundText.text = roundString;

                    Debug.Log("Round " + RoundNum + " Starting!");


                    Order = Order.OrderByDescending(unit => unit.speed).ToList(); // set the order of participants by speed

                    ReorderTurnIcons(); // <-- Reorder icons in UI hierarchy

                    PickNextTurn(); //based on speed, pick the next turn. 
                }
                break;


            case BattleState.PlayerTurn:
                if (!isPlayerTurn)
                {
                    isPlayerTurn = true;
                    isJasperTurn = false;
                    isEnemyTurn = false;
                    isYaelTurn = false;
                    StartPlayerTurn();
                }
                break;

            case BattleState.JasperTurn:
                if (!isJasperTurn)
                {
                    isPlayerTurn = false;
                    isJasperTurn = true;
                    isEnemyTurn = false;
                    isYaelTurn = false;
                    
                    StartJasperTurn();

                }
                break;

            case BattleState.YaelTurn:
                if (!isYaelTurn)
                {
                    isPlayerTurn = false;
                    isJasperTurn = false;
                    isEnemyTurn = false;
                    isYaelTurn = true;
                    
                    StartYaelTurn();

                }
                break;

            case BattleState.EnemyTurn:
                if (!isEnemyTurn)
                {
                    isPlayerTurn = false;
                    isJasperTurn = false;
                    isEnemyTurn = true;
                    isYaelTurn = false;
                    StartEnemyTurn();
                }
                break;
            case BattleState.RoundBuffer:
                StartCoroutine(RoundBuffer());
                break;

            case BattleState.RoundEnd:
                StartCoroutine(RoundEndLogic());
                break;

            case BattleState.Lost:
                break;

            case BattleState.Won:
                break;
        }
    }

    public void FillTurnRoster()
    {
        Debug.Log("Filling Turn Roster...");
        Order = GameObject.FindObjectsOfType<Unit>().ToList();
        hasFilledRoster = true; // when that's done, set roster filled to true.

        

        
    }

    IEnumerator RoundEndLogic()
    {
        roundStarted = false;
        SetAllTurnsFalse();
        Order.Clear();
       
        hasFilledRoster = false;
        yield return new WaitForSeconds(0.5f);
        state = BattleState.RoundBuffer;
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBS);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject JasperGO = Instantiate(jasperPrefab, JasperBS);
        JasperUnit = JasperGO.GetComponent<Unit>();

        GameObject YaelGO = Instantiate(yaelPrefab, YaelBS);
        YaelUnit = YaelGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBS);
        enemyUnit = enemyGO.GetComponent<Unit>();

        yield return new WaitForSeconds(2f);

       // state = BattleState.PlayerTurn;
    }

    public void SetAllTurnsFalse()
    {
        isPlayerTurn = false;
        isEnemyTurn = false;
        isYaelTurn = false;
        isJasperTurn = false;
    }

    public void ReorderTurnIcons()
    {
        for (int i = 0; i < Order.Count; i++)
        {
            Unit unit = Order[i];

            // Find the icon that matches this unit
            GameObject matchingIcon = IconsList.Find(icon =>
            {
                TurnIcon iconComponent = icon.GetComponent<TurnIcon>();
                return iconComponent != null && iconComponent.unit == unit;
            });

            if (matchingIcon != null)
            {
                // Set sibling index to match the turn order
                matchingIcon.transform.SetSiblingIndex(i);
            }
            else
            {
                Debug.LogWarning("No matching icon found for unit: " + unit.unitName);
            }
        }
    }


    IEnumerator RoundBuffer()
    {
        yield return new WaitForSeconds(2);
        state = BattleState.GetParticipants;
    }

    public void PickNextTurn()
    {
        if (Order.Count == 0)
        {
            Debug.Log("No more units in turn order.");
            state = BattleState.RoundEnd;
            return;
        }

        Unit currentUnit = Order[0];
        string unitName = currentUnit.unitName;

        switch (unitName)
        {
            case "Player":
                state = BattleState.PlayerTurn;
                break;
            case "Jasper":
                state = BattleState.JasperTurn;
                break;
            case "Yael":
                state = BattleState.YaelTurn;
                break;
            case "RAT":
                state = BattleState.EnemyTurn;
                break;
            default:
                Debug.LogWarning("Unknown unit turn: " + unitName);
                break;
        }
        Debug.Log(unitName + "'s Turn");
    }


    public void EndTurn()
    {
        if (Order.Count >= 0)
        {
            RemovePreviousTurn();
            PickNextTurn();
        }
        else
        {
            Debug.Log("EndTurn called, but Order list is empty. Ending round.");
            RoundEndLogic();
        }
    }


    public void RemovePreviousTurn()
    {
        if (Order.Count > 0)
        {
            Order.RemoveAt(0);
        }
        else
        {
            Debug.LogWarning("Attempted to remove unit from empty Order list.");
        }
    }

    public void StartPlayerTurn()
    {
        state = BattleState.PlayerTurn;
        _playerName.text = "PLAYER";
        playerBattleOptions.SetActive(true);
    }

    public void StartEnemyTurn()
    {
        state = BattleState.EnemyTurn;
        _playerName.text = "RAT";
        StartCoroutine(EnemyLogic());
    }

    IEnumerator EnemyLogic()
    {
        Debug.Log("Enemy attacks!");
        yield return new WaitForSeconds(1f);
        _enemyManager.EnemyAttack();
        // After enemy attack animation / logic
        EndTurn(); // <-- Enemy ends turn cleanly after action
    }

    public void StartJasperTurn()
    {
        state = BattleState.JasperTurn;
        _playerName.text = "JASPER";


        yaelBattleOptions.SetActive(false);
        playerBattleOptions.SetActive(false);
        jasperBattleOptions.SetActive(true);
    }

    public void StartYaelTurn()
    {
        state = BattleState.YaelTurn;
        _playerName.text = "YAEL";
        yaelBattleOptions.SetActive(true);
        playerBattleOptions.SetActive(false);
        jasperBattleOptions.SetActive(false);

    }

    public void EndBattle()
    {
      //  state = BattleState.Start;
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
    }

    //void ChangePointer()
    //{
    //    Vector3 targetPosition = enemies[currentEnemyIndex].transform.position;

    //    // Add the Y offset to position the pointer above the enemy
    //    targetPosition.y += _hoverHeight;

    //    // Move the pointer to the position with Y offset
    //    Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetPosition);
    //    pointer.position = screenPosition;
    //}
}
