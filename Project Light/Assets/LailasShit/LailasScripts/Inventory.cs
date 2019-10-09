using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : ItemContainer
{
    [SerializeField] List<Item> startingItems;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemTooltip itemToolTip;


    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;
    public event Action<BaseItemSlot> OnBeginDragE;
    public event Action<BaseItemSlot> OnEndDragE;
    public event Action<BaseItemSlot> OnDragE;
    public event Action<BaseItemSlot> OnDropE;

    void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            itemSlots[i].OnRightClickEvent += OnRightClickEvent;
            itemSlots[i].OnBeginDragE += OnBeginDragE;
            itemSlots[i].OnEndDragE += OnEndDragE;
            itemSlots[i].OnDragE += OnDragE;
            itemSlots[i].OnDropE += OnDropE;
        }
        SetStartingItems();
    }
    private void OnValidate()
    {
        if (itemsParent != null)
        {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
        }
        SetStartingItems();
    }

    private void SetStartingItems()
    {
        int i = 0;
        for (; i < startingItems.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].item = startingItems[i].GetCopy();
            itemSlots[i].Amount = 1;
        }
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].item = null;
            itemSlots[i].Amount = 0;
        }
    }

    //public bool ContainItem(Item item)
    //{
    //    for (int i = 0; i < itemSlots.Length; i++)
    //    {
    //        if (itemSlots[i].item == item)
    //        {
    //            return true;
    //        }

    //    }
    //    return false;
    //}


}
