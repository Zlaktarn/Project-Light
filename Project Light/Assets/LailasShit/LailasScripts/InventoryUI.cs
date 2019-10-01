using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    public Transform ItemParent;
    InventorySlot[] slots;
    public GameObject inventoryUI;

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
}
