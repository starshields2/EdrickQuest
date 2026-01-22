using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerActions : MonoBehaviour
{
    public BattleSystem bSystem; //overall battle manager
    public TensionCounter _tensCounter; //tension counter

    public int _diffuseModifier; //how much to reduce tension by
    public int _speedModifier; // how much to increase speed by
    public int _healModifier; // how much to heal
    public int _damageModifier; // how much to hurt

    [Header("EFFECTS")]
    public GameObject _diffuseParticle;
    public GameObject _healParticle;

    public RectTransform _pointer;
    public float _hoverHeight = 2.0f;

    public GameObject[] _allies;
    public GameObject[] _enemies;

    public int currentAllyIndex = 0;
    public int currentEnemyIndex = 0;

    public GameObject selectedAlly;
    public GameObject selectedEnemy;
    public Unit selectedUnit; 

    private bool isSelectingAlly = false;
    public bool isSelectingEnemy = false;

    void Update()
    {
        // Ally selection input
        if (isSelectingAlly)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("selecting next ally");
                PlayerSelectNextAlly();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                PlayerSelectPreviousAlly();
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                ConfirmAllySelection();
            }
        }

        // Enemy selection input
        if (isSelectingEnemy)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log("selecting next enemy");
                PlayerSelectNextEnemy();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                PlayerSelectPreviousEnemy();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                ConfirmEnemySelection();
            }
        }
    }

    //Gen Options:
    public void Diffuse()
    {
        StartCoroutine(DiffuseCoroutine());
    }

    public void Defend()
    {
        StartCoroutine(DefendCoroutine());
    }

    public IEnumerator DiffuseCoroutine()
    {
        _tensCounter._tension -= _diffuseModifier;
        _diffuseParticle.SetActive(true);
        yield return new WaitForSeconds(2);
        _diffuseParticle.SetActive(false);
        bSystem.EndTurn();
    }

    public IEnumerator DefendCoroutine()
    {
       //function: choose an ally to defend. They should take -PROT% damage next time they are targeted. remove one tension for helping a friend :) 

        isSelectingAlly = true;

        // Wait until the player confirms an ally
        while (isSelectingAlly)
        {
            yield return null;
        }

        // Example: Apply protection bonus the selected unit
        if (selectedUnit != null)
        {
            
            Debug.Log("Defending: " + selectedUnit.unitName);

            _tensCounter._tension -= 1; 

            Companion _thisCompanion = selectedUnit.GetComponent<Companion>();

            if (_thisCompanion.specialBlocked)
            {
                Debug.Log("ERROR: " + selectedUnit + "IS ATTRIBUTE BLOCKED!");
            }
            if(_thisCompanion.specialBlocked == false)
            {
                selectedUnit.protect += 3f;
                selectedUnit.isProtected = true;
            }
            else
            {
                Debug.Log(selectedUnit + "ERROR");
            }



        }

        yield return new WaitForSeconds(2);
        bSystem.EndTurn();
    }

    public void AddComboToRoster()
    {
        //adds combo partner to roster. 
        Debug.Log("Adding player to roster");
        StartCoroutine(AddCombo());

    }

    public IEnumerator AddCombo()
    {
        isSelectingAlly = true;

        // Wait until the player confirms an ally
        while (isSelectingAlly)
        {
            yield return null;
        }

        // Example: Apply an effect to the selected unit
        if (selectedUnit != null)
        {
            _tensCounter._currentTetherPoints -= 3;
            Debug.Log("selected: " + selectedUnit.unitName);


            Companion _thisCompanion = selectedUnit.GetComponent<Companion>();
            Debug.Log(_thisCompanion);
            if (_thisCompanion.specialBlocked)
            {
                Debug.Log("ERROR:" + selectedUnit + "IS ATTRIBUTE BLOCKED!");
            }
            else
            {
                if (_thisCompanion.comboReady)
                {
                    Debug.Log("ADDING " + selectedUnit + "TO ROSTER!");
                    bSystem.Order.Insert(1, selectedUnit);
                }
            }



        }

        yield return new WaitForSeconds(2);
       
        bSystem.EndTurn();
    }
    public void Encourage()
    {
        StartCoroutine(EncourageModifier());
    }

    public void Assess()
    {
        StartCoroutine(AssessStart());
    }

    public IEnumerator AssessStart()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Assessing -");
        bSystem.EndTurn();
    }

    public IEnumerator EncourageModifier()
    {
        isSelectingAlly = true;

        // Wait until the player confirms an ally
        while (isSelectingAlly)
        {
            yield return null;
        }

        // Example: Apply an effect to the selected unit
        if (selectedUnit != null)
        {
            Debug.Log("Encouraging: " + selectedUnit.unitName);
            // Example: Heal the unit
            selectedUnit.speed += _speedModifier;

        }

        yield return new WaitForSeconds(2);
        bSystem.EndTurn();
    }

    public void Taunt()
    {
        StartCoroutine(TauntEnemy());
    }

    IEnumerator TauntEnemy()
    {
        Debug.Log("Taunting Start...");
        //animations and shit
        //Get the selected enemy and add a bool to it so it can only focus player rn
        yield return new WaitForSeconds(2f);
        bSystem.EndTurn();
    }

    public void Pass()
    {
        StartCoroutine(PassTurn());
    }

    IEnumerator PassTurn()
    {
        _tensCounter._tension += 2; //(replace with a percentage modifier)
        Debug.Log("Passing...");
        //animations and shit
        yield return new WaitForSeconds(2f);
        bSystem.EndTurn();
    }

    //Yael Special Options: 

    public void BasicHeal()
    {
        StartCoroutine(BasicHealer());
    }

    //Jasper Special Options: 

    public void BasicAttack()
    {
        Debug.Log("BasicAttack() called");

            StartCoroutine(BasicAttacker());
              
    }


    public IEnumerator BasicHealer()
    {
        isSelectingAlly = true;

        // Wait until the player confirms an ally
        while (isSelectingAlly)
        {
            yield return null;
        }

        // Example: Apply an effect to the selected unit
        if (selectedUnit != null)
        {
            _tensCounter._currentTetherPoints -= 3;
            Debug.Log("Healing: " + selectedUnit.unitName);
            

            Companion _thisCompanion = selectedUnit.GetComponent<Companion>();

            if(_thisCompanion.specialBlocked)
            {
                Debug.Log("ERROR:" + selectedUnit + "IS ATTRIBUTE BLOCKED!");
            }
            else
            {
                selectedUnit.currentHP += _healModifier;
                _healParticle.SetActive(true);
            }

            

        }

        yield return new WaitForSeconds(2);
        _healParticle.SetActive(false);
        bSystem.EndTurn();
    }


    public IEnumerator BasicAttacker()
    {
        Debug.Log("BasicAttack");

        isSelectingEnemy = true;

        // Wait until the player confirms an ally
        while (isSelectingEnemy)
        {
            yield return null;
        }

        // Example: Apply an effect to the selected unit
        if (selectedUnit != null)
        {
            Debug.Log("Attacking: " + selectedUnit.unitName);
           
            selectedUnit.TakeDamage();
        }

        yield return new WaitForSeconds(2);
        bSystem.EndTurn();
    }

    public void PlayerSelectNextAlly()
    {
        currentAllyIndex++;
        if (currentAllyIndex >= _allies.Length)
        {
            currentAllyIndex = 0;
        }

        SetPointerPositionAlly();
    }

    public void PlayerSelectPreviousAlly()
    {
        currentAllyIndex--;
        if (currentAllyIndex < 0)
        {
            currentAllyIndex = _allies.Length - 1;
        }

        SetPointerPositionAlly();
    }

    public void PlayerSelectNextEnemy()
    {
        currentEnemyIndex++;
        if (currentEnemyIndex >= _enemies.Length)
        {
            currentEnemyIndex   = 0;
        }

        SetPointerPositionEnemy();
    }

    public void PlayerSelectPreviousEnemy()
    {
        currentEnemyIndex--;
        if (currentEnemyIndex < 0)
        {
            currentEnemyIndex = _enemies.Length - 1;
            Debug.Log(currentEnemyIndex);
        }

        SetPointerPositionEnemy();
    }

    private void SetPointerPositionAlly()
    {
        selectedAlly = _allies[currentAllyIndex];
        Vector3 targetPosition = selectedAlly.transform.position;
        targetPosition.y += _hoverHeight;

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetPosition);
        _pointer.position = screenPosition;
    }

    private void SetPointerPositionEnemy()
    {
        selectedEnemy = _enemies[currentEnemyIndex];
        Vector3 targetPosition = selectedEnemy.transform.position;
        targetPosition.y += _hoverHeight;

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetPosition);
        _pointer.position = screenPosition;
    }


    private void ConfirmAllySelection()
    {
        isSelectingAlly = false;

        selectedAlly = _allies[currentAllyIndex];
        selectedUnit = selectedAlly.GetComponent<Unit>();

        if (selectedUnit != null)
        {
            Debug.Log("Selected Unit: " + selectedUnit.unitName);
        }
        else
        {
            Debug.LogWarning("Selected ally does not have a Unit component!");
        }
    }

    private void ConfirmEnemySelection()
    {
        isSelectingEnemy = false;

        selectedEnemy = _enemies[currentEnemyIndex];
        selectedUnit = selectedEnemy.GetComponent<Unit>();

        if (selectedUnit != null)
        {
            Debug.Log("Selected Unit: " + selectedUnit.unitName + " | speed : " + selectedUnit.speed);
        }
        else
        {
            Debug.LogWarning("Selected enemy does not have a Unit component!");
        }
    }
}
