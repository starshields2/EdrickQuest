using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryManager : MonoBehaviour
{
    public int playerGold;
    [SerializeField] private GameObject slotHolder;
    [SerializeField] private ItemClass itemToAdd;
    [SerializeField] private ItemClass itemToRemove;
    public List<SlotClass> items = new List<SlotClass>();
    private GameObject[] slots;

    public void Start()
    {
        slots = new GameObject[slotHolder.transform.childCount];

        //set all slots
        for(int i = 0; i < slotHolder.transform.childCount; i++)
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
       
        
        Add(itemToAdd);
        Remove(itemToRemove);
        RefreshUI();
    }
    [ContextMenu("Refresh")]
    public void RefreshUI()
    {
       for (int i = 0; i < slots.Length; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = items[i].GetQuantity().ToString();
                
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
           
        }
    }
    [ContextMenu("AddItem")]
    public bool Add(ItemClass item)
    {
        Debug.Log("Adding Item");
        //    items.Add(item);
        //check if inventory contains item

        SlotClass slot = Contains(item);
        if (slot != null && slot.GetItem().isStackable)
        {
            slot.AddQuantity(1);
            Debug.Log("Added stacked item");
        }
           
        else
        {
            if(items.Count < slots.Length)
            {
                items.Add(new SlotClass(item, 1));
                Debug.Log("Added unstackable item");
            }
            
            else
            {
                return false;
            }

        }
        RefreshUI();
        return true;
    }
    [ContextMenu("RemoveItem")]
    public bool Remove(ItemClass item)
    {
        //   items.Remove(item);
        SlotClass temp = Contains(item);
        if (temp != null)
        {
            if (temp.GetQuantity() > 1)
                temp.SubQuantity(1);
            else
            {
                SlotClass slotToRemove = new SlotClass();
                foreach (SlotClass slot in items)
                {
                    if (slot.GetItem() == item)
                    {
                        slotToRemove = slot;
                        break;
                    }
                }
                items.Remove(slotToRemove);
            }
        }
        else
        {
            return false;
        }
        RefreshUI();
        return true;
    }

    [ContextMenu("AddNewItem")]
    public void AddNewItem()
    {
        Add(itemToAdd);   
    }
    [ContextMenu("RemoveItem")]
    public void RemoveNewItem()
    {
        Remove(itemToRemove);
    }

    public SlotClass Contains(ItemClass item)
    {
        foreach (SlotClass slot in items)
        {
            if (slot.GetItem() == item)
                return slot;
        }
        return null;
    }
}
