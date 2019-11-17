using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] List<ItemScript> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlotScript[] itemSlots;

    private void OnValidate()
    {
        if (itemsParent != null)
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlotScript>();

        RefreshUI();
    }

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
}
