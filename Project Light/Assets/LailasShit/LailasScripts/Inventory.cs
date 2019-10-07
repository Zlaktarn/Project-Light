using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour, IItemConatiner
{
    [SerializeField] List<Item> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;

    public event Action<Item> OnItemRightclickevent;

    void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnRightClickEvent += OnItemRightclickevent;
        }
        RefreshUI();
    }
    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        }
        RefreshUI();
    }

    //private void SetStartingItem()
    //{
    //    for (int i = 0; i < SetStartingItem.Length; i++)
    //    {

    //    }
    //}
    private void RefreshUI()
    {
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].item = items[i];
        }
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].item = null;
        }
    }
    public bool AddItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == null)
            {
                itemSlots[i].item = item;
                return true;
            }

        }
        return false;
    }
    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == item)
            {
                itemSlots[i].item = null;
                return true;
            }

        }
        return false;
    }
    public bool IsFull()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].item == null)
            {
                return false;
            }
            
        }
        return true;
    }

    public bool ContainItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == item)
            {
                return true;
            }

        }
        return false;
    }

    public int ItemCount(Item item)
    {
        int number = 0;
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == item)
            {
                number++;
            }

        }
        return number;
    }
}
