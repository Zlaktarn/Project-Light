using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct ItemAmount
{
    public Item item;
    [Range(1, 3)]
    public int Amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Result;

    public bool CantCraft(IItemConatiner itemConatiner)
    {
        foreach (ItemAmount itemAmount in Materials)
        {
            if(itemConatiner.ItemCount(itemAmount.item) < itemAmount.Amount)
            {
                return false;
            }
        }
        return true;
    }
    public void Craftable(IItemConatiner itemConatiner)
    {
        if(CantCraft(itemConatiner))
        {
            foreach (ItemAmount itemAmount in Materials)
            {
                for (int i = 0; i < itemAmount.Amount; i++)
                {
                    itemConatiner.RemoveItem(itemAmount.item);
                }
            }
            foreach (ItemAmount itemAmount in Result)
            {
                for (int i = 0; i < itemAmount.Amount; i++)
                {
                    itemConatiner.AddItem(itemAmount.item);
                }
            }
        }
    }

}
