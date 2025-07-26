using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    public TensionCounter _tensionCounter;
    public Unit _thisUnit;
    public Unit _otherUnit;
    public bool comboReady;
    public bool defendBlocked;
    public bool specialBlocked;
    public bool canTakeDeathblow;

    [System.Serializable] public enum Attribute
    {
         Neutral,
         Avoidant,
         Competitive,
         Codependent,
         Respectful,
         Collaborative,
         Amorous
    }
    public Attribute attribute;

    void Start()
    {
        _thisUnit = this.gameObject.GetComponent<Unit>();
        if(_thisUnit.name == "Yael")
        {
            _otherUnit = GameObject.FindWithTag("Jasper").GetComponent<Unit>();
        }
        if (_thisUnit.name == "Jasper")
        {
            _otherUnit = GameObject.FindWithTag("Yael").GetComponent<Unit>();
        }
    }

    void Awake()
    {
        CheckAttribute();
    }
[ContextMenu("Get Atribute")]
  public void AcquireAttribute()
    {
        Debug.Log("Acquiring Attribute");
        if(_tensionCounter._influence <= 30)
        {
            attribute = (Attribute)Random.Range(1, 2);
        }

        if (_tensionCounter._influence >= 31 && _tensionCounter._influence <= 60)
        {
            attribute = (Attribute)Random.Range(3, 5);
        }


        if (_tensionCounter._influence >= 61)
        {
            attribute = (Attribute)Random.Range(4, 6);
        }
        CheckAttribute();
    }

    [ContextMenu("Test Atribute")]
    void CheckAttribute()
    {
        switch (attribute)
        {
            case Attribute.Neutral:
                break;
            case Attribute.Avoidant:
                _thisUnit.speed = _thisUnit.speed - 3;
                    //defendBlocked = true;s
                    //specialBlocked = true;
                break;

            case Attribute.Competitive:
                _thisUnit.speed = _otherUnit.speed + 3;
                break;
            case Attribute.Collaborative:
               

                break;
            case Attribute.Codependent:
                _thisUnit.speed = _otherUnit.speed;
                _thisUnit.health = _otherUnit.health;
                break;

            case Attribute.Respectful:
               //whatever this was supposed to be
                break;

            case Attribute.Amorous:
                //can take deathblows
                //health up

                break;
            default:
                Debug.LogWarning("Unknown attribute:" + attribute);
                break;
        }
    }

  
}
