using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool isInventory;
    public bool isEscape;
    public GameObject inventoryHolder;
    public GameObject _UI;
    // Start is called before the first frame update
    void Start()
    {
        isInventory = false;
        isEscape = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeInventoryStatus();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeUIStatus();
        }

        if (isInventory)
        {
            inventoryHolder.SetActive(true);
        }
        if (!isInventory)
        {
            inventoryHolder.SetActive(false);
        }

        if (isEscape)
        {
           _UI.SetActive(true);
        }
        if (!isEscape)
        {
           _UI.SetActive(false);
        }
    }

    public void ChangeInventoryStatus()
    {
        isInventory = !isInventory;
    }
    public void ChangeUIStatus()
    {
        isEscape = !isEscape;
    }
}
