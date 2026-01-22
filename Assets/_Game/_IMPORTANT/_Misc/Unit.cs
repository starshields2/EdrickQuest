using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unit : MonoBehaviour
{
    public string unitName;
    public GameObject unitIcon;

    [Header("Status")]
    public bool isDefending;
    public bool isProtected;
    public GameObject Shield;
    public GameObject ProtectedIcon;
    public TextMeshProUGUI damageString;

    [Header("Stats")]
    public int attack;
    public float speed;
    public float defense;
    public float protect;

    public int damageRange;
    public float ogDamage;

    [Header("HP Settings")]
    public float maxHP;
    public float DZThreshold;

    public float currentHP;
    public Slider health;
    public bool DZTon;


    void Update()
    {
        health.value = currentHP;
    }

    public void TakeDamage()
    {
        EnemyBattleManager _enemyManager = GameObject.Find("BATTLE SYSTEM").GetComponent<EnemyBattleManager>();
        Unit attackingEnemy = _enemyManager.enemies[0].GetComponent<Unit>();

        float damage = attackingEnemy.ogDamage;
        float finalDamage = damage;

        // Apply defense and protect
        if (isProtected || isDefending)
        {
            finalDamage = (damage * defense) - protect;
            if (finalDamage < 0) finalDamage = 0;

            Debug.Log($"PROTECTED! DAMAGE TO {unitName} REDUCED FROM: {damage} TO {finalDamage}");
        }

        // Apply damage only once
        currentHP -= finalDamage;

        // Show damage
        StartCoroutine(ShowDamage(finalDamage));

        // Check for death
        if (currentHP <= 0)
        {
            StartCoroutine(Die());
        }
    }


    private IEnumerator ShowDamage(float damage)
    {
        damageString.text = damage.ToString();
        damageString.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        damageString.gameObject.SetActive(false);
    }
    private IEnumerator Die()
    {
        Debug.Log("DEATH " + gameObject.name);
        //die stuff.
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void DamageDebuff(float damage)
    {
        int dmgLost = Random.Range(1, 3);
        damage = damage - dmgLost;
    }

    public void DamageBuff(float damage)
    {
        int dmgMod = Random.Range(0, 3);
        damage = damage + dmgMod;
    }

    public void Defend()
    {
        isDefending = true;
        Shield.SetActive(true);

    }

    public void StopDefending()
    {
        isDefending = false;
        Shield.SetActive(false);
    }

    public void Protected()
    {
        isProtected = true;
        ProtectedIcon.SetActive(true);
    }

    public void Heal()
    {
        currentHP += Random.Range(2, 6);
        Debug.Log(this.gameObject.name + "healed to: " + currentHP);
    }
}
