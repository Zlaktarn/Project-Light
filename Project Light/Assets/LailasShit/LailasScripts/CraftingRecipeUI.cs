﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] RectTransform arrowParent;
    [SerializeField] BaseItemSlot[] itemSlots;

    [Header("Public Variables")]
    public ItemContainer itemContainer;

    private CraftingRecipe craftingrecipe;
    public CraftingRecipe Craftingrecipe
    {
        get { return craftingrecipe; }
        set { SetCraftingRecipe(value); }
    }

    public event Action<BaseItemSlot> OnPointerEnterE;
    public event Action<BaseItemSlot> OnPointerExitE;

    private void OnValidate()
    {
        itemSlots = GetComponentsInChildren<BaseItemSlot>(includeInactive: true);
    }

    void Start()
    {
        foreach (BaseItemSlot itemSlot in itemSlots)
        {
            itemSlot.OnPointerEnterEvent += OnPointerEnterE;
            itemSlot.OnPointerExitEvent += OnPointerExitE;
            //itemSlot.OnPointerEnterEvent += slot => OnPointerEnterE(slot);
            //itemSlot.OnPointerExitEvent += slot => OnPointerExitE(slot);

        }
    }

    public void OnCraftButtonClick()
    {
        if (craftingrecipe != null && itemContainer != null)
        {
            craftingrecipe.Craft(itemContainer);
        }
    }

    private void SetCraftingRecipe(CraftingRecipe newCraftingRec)
    {
        craftingrecipe = newCraftingRec;

        if(craftingrecipe != null)
        {
            int slotIndex = 0;
            slotIndex = SetSlots(craftingrecipe.Materials, slotIndex);
            arrowParent.SetSiblingIndex(slotIndex);
            slotIndex = SetSlots(craftingrecipe.Results, slotIndex);

            for (int i = slotIndex; i < itemSlots.Length; i++)
            {
                itemSlots[i].transform.parent.gameObject.SetActive(false);
                slotIndex++;
            }
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

    private int SetSlots(IList<ItemAmount> itemAmountList, int slotIndex)
    {
        for (int i = 0; i < itemAmountList.Count; i++, slotIndex++)
        {
            ItemAmount itemAmount = itemAmountList[i];
            BaseItemSlot itemSlot = itemSlots[slotIndex];

            itemSlot.item = itemAmount.Item;
            itemSlot.Amount = itemAmount.Amount;
            itemSlot.transform.parent.gameObject.SetActive(true);
        }
        return slotIndex;
    }

}
