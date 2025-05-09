using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unit : MonoBehaviour
{
    public string unitName;

    [Header("Status")]
    public bool isDefending;
    public bool isProtected;
    public bool protecting;
    public bool dangerZone;


    public GameObject Shield;
    public GameObject ProtectedIcon;
    public TextMeshProUGUI damageString;

    [Header("Stats")]

    public float attack;
    public float speed;
    public float protect;
    public float crit;
    public float defense;
    public float dangerZoneHP;


    public int damageRange;
    public int ogDamage;
    public int critDamage;

    [Header("HP Settings")]
    public float maxHP;
    public float currentHP;
    public Slider health;


    void Update()
    {
        health.value = currentHP;
        if(currentHP <= dangerZoneHP)
        {
            dangerZone = true;
        }
        else
        {
            dangerZone = false;
        }
    }

    public void TakeDamage(int damage)
    {
        damage = Random.Range(0,7);
        print(damage);
        if (currentHP <= 0)
        {
            StartCoroutine(Die());
        }

        if (isDefending)
        {
            currentHP -= (damage - defense); // Reduce the damage by defense stat
        }
        else
        {
            currentHP -= damage; // Full damage when not defending
        }

        if (isProtected)
        {
            damage = 0;
            Debug.Log("PROTECTED!");
        }

        StartCoroutine(ShowDamage(damage));


        
    }

    private IEnumerator ShowDamage(int damage)
    {
        damageString.text = damage.ToString();
        damageString.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        damageString.gameObject.SetActive(false);
    }
    private IEnumerator Die()
    {
        Debug.Log("died " + gameObject.name);
        //die stuff.
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void DamageDebuff(int damage)
    {
        int dmgLost = Random.Range(1, 3);
        damage = damage - dmgLost;
    }

    public void DamageBuff(int damage)
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
