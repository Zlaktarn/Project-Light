using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour, ItemContainer
{
    Inventory inventory;
    public Transform ItemParent;
    public InventorySlot[] slots;
    public GameObject inventoryUI;
    public List<Item> items = new List<Item>();

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangeCallBack += UpdateUI;
        slots = ItemParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);               
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

    }

    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item)
            {
                slots[i].item = null;
                return true;
            }
        }
        return false;
    }
    public bool AddItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].item = item;
                return true;
            }
        }
        return false;
    }

    public bool IsFull()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return false;
            }
        }
        return true;
    }
    public bool ContainsItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item)
            {
                return true;
            }
        }
        return false;
    }

    public int ItemCount(Item item)
    {
        int num = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item)
            {
                num++;
            }
        }
        return num;
    }
}
