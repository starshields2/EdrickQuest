using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerActions : MonoBehaviour
{
    public BattleSystem bSystem;
    public TensionCounter _tensCounter;
    public int _diffuseModifier;
    public int _speedModifier;
    public int _healModifier;
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
    public Unit selectedUnit; // Store the Unit component of selectedAlly

    private bool isSelectingAlly = false;
    private bool isSelectingEnemy = false;

    void Update()
    {
        if (!isSelectingAlly) return;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
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
    //Player Options:
    public void Diffuse()
    {
        StartCoroutine(DiffuseCoroutine());
    }

    public IEnumerator DiffuseCoroutine()
    {
        _tensCounter._tension -= _diffuseModifier;
        _diffuseParticle.SetActive(true);
        yield return new WaitForSeconds(2);
        _diffuseParticle.SetActive(false);
        bSystem.EndTurn();
    }

    public void Encourage()
    {
        StartCoroutine(EncourageModifier());
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
            _healParticle.SetActive(true);
            selectedUnit.currentHP += _healModifier;

        }

        yield return new WaitForSeconds(2);
        _healParticle.SetActive(false);
        bSystem.EndTurn();
    }


    public IEnumerator BasicAttacker()
    {
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
            // Example: Heal the unit
            selectedUnit.currentHP += _healModifier;

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

        SetPointerPosition();
    }

    public void PlayerSelectPreviousAlly()
    {
        currentAllyIndex--;
        if (currentAllyIndex < 0)
        {
            currentAllyIndex = _allies.Length - 1;
        }

        SetPointerPosition();
    }

    public void PlayerSelectNextEnemy()
    {
        currentEnemyIndex++;
        if (currentEnemyIndex >= _enemies.Length)
        {
            currentEnemyIndex   = 0;
        }

        SetPointerPosition();
    }

    public void PlayerSelectPreviousEnemy()
    {
        currentEnemyIndex--;
        if (currentEnemyIndex < 0)
        {
            currentEnemyIndex = _enemies.Length - 1;
        }

        SetPointerPosition();
    }

    private void SetPointerPosition()
    {
        selectedAlly = _allies[currentAllyIndex];
        Vector3 targetPosition = selectedAlly.transform.position;
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
            Debug.Log("Selected Unit: " + selectedUnit.unitName + " | speed : " + selectedUnit.speed);
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
