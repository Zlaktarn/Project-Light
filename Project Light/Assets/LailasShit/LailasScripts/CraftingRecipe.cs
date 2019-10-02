using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct ItemAmount
{
    public Item item;
    [Range(1, 99)]
    public int amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;

    public bool Craftable(ItemContainer itemC)
    {
        foreach (ItemAmount itemAmount in Materials)
        {
            if(itemC.ItemCount(itemAmount.item) < itemAmount.amount)
            {
                return false;
            }
        }
        return true;
    }
    public bool Craft(ItemContainer itemC)
    {
        if(Craftable(itemC))
        {
            foreach (ItemAmount itemAmount in Materials)
            {
                for (int i = 0; i < itemAmount.amount; i++)
                {
                    itemC.RemoveItem(itemAmount.item);
                }
            }

            foreach (ItemAmount itemAmount in Results)
            {
                for (int i = 0; i < itemAmount.amount; i++)
                {
                    itemC.AddItem(itemAmount.item);
                }
            }
        }
        return true;
    }
}
