using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPickup : MonoBehaviour
{
    public InventoryManager _manager;
    public ItemClass _itemToPickup;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(PickedUp());
            
        }
    }

    public IEnumerator PickedUp()
    {
        yield return new WaitForSeconds(0.2f);
        if(_manager != null)
        {
            _manager.Add(_itemToPickup);
        }
        Destroy(gameObject);
    }
}
