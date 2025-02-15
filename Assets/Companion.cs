using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    public TensionCounter _tensionCounter;

    [System.Serializable] public enum Attribute
    {
         Neutral,
         Avoidant,
         Competitive,
         Compromising,
         Accomodating,
         Collaborative
    }
    public Attribute attribute;


    void Awake()
    {
        attribute = Attribute.Neutral;
    }

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
            attribute = (Attribute)Random.Range(4, 5);
        }
    }
}
