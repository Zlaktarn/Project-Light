using System;
using System.Collections.Generic;
using UnityEngine;


public class CraftingRecipeUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] InventorySlot[] invSlot;

    [Header("References")]
    public ItemContainer Itemcontainer;

    private CraftingRecipe craftingRec;

    //public CraftingRecipe CraftingRecipe
    //{
    //    get { return craftingRec; }
    //    set {SetCraftingRecipe(value); }
    //}

    public event Action<InventorySlot> OnPointerEnterEvent;
    public event Action<InventorySlot> OnPointerExitEvent;

    private void OnValidate()
    {
        invSlot = GetComponentsInChildren<InventorySlot>(includeInactive: true);
    }

    private void Start()
    {
        foreach (InventorySlot inventorySlot in invSlot)
        {
        }
    }

    void Update()
    {
        
    }
}
