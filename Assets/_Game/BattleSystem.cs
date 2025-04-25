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
    public EnemyBattleManager manager;
    public Companion[] companions;

    [Header("TurnOrder")]
    public List<Unit> Order = new List<Unit>();
    [Header("TurnOrderIcons")]
    public GameObject[] Icons;

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
                BattleCanvas.SetActive(true);
                state = BattleState.GetParticipants;
                break;

            case BattleState.GetParticipants:
                roundStarted = false;
                foreach (GameObject active in BattleUnits)
                {
                    active.SetActive(true);
                }


                Order = GameObject.FindObjectsOfType<Unit>().ToList();
                state = BattleState.RoundStart;
                Debug.Log("Filling Turn Roster...");
                break;

            case BattleState.RoundStart:
                if (!roundStarted)
                {
                    roundStarted = true;
                    Debug.Log("Round Starting!");
                    Order = Order.OrderByDescending(unit => unit.speed).ToList();
                    Debug.Log("Sorted Roster by Speed.");

                    PickNextTurn();
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
                    RemovePreviousTurn();
                    PickNextTurn();
                }
                break;
            case BattleState.RoundBuffer:
                StartCoroutine(RoundBuffer());
                break;

            case BattleState.RoundEnd:
                Order.Clear();
                roundStarted = false;
                state = BattleState.RoundBuffer;
                break;

            case BattleState.Lost:
                break;

            case BattleState.Won:
                break;
        }
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
        print(unitName + "'s Turn");      
    }

    public void RemovePreviousTurn()
    {
        Order.RemoveAt(0);
    }
    public void StartBattle()
    {
        state = BattleState.Start;
    }

    public void StartPlayerTurn()
    {
        state = BattleState.PlayerTurn;
        _playerName.text = "PLAYER";

        Vector3 position = playerBS.position;
        position.x -= 0.3f;
        playerBS.position = position;

      
        playerBattleOptions.SetActive(true);
    }

    public void StartEnemyTurn()
    {
        state = BattleState.EnemyTurn;
       
    }

    public void StartJasperTurn()
    {
        state = BattleState.JasperTurn;
        _playerName.text = "JASPER";



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
}
