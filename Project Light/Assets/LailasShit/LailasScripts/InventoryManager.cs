using UnityEngine;

public class InventoryManager : MonoBehaviour //Charachter clas
{
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;

    void Start()
    {
        inventory.OnItemRightclickevent += EquipFromInventory;
        equipmentPanel.OnItemRightclickevent += UnequipFromEquipPanel;
    }


    private void EquipFromInventory(Item item)
    {
        if(item is EquippableItem)
        {
            Equip((EquippableItem)item);
        }
    }

    private void UnequipFromEquipPanel(Item item)
    {
        if (item is EquippableItem)
        {
            Unequip((EquippableItem)item);
        }
    }


    public void Equip(EquippableItem item)
    {
        if(inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if(equipmentPanel.AddItem(item, out previousItem))
            {
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                }

            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }
    public void Unequip(EquippableItem item)
    {
        if(!inventory.IsFull() )
        {
            equipmentPanel.RemoveItem(item);
            inventory.AddItem(item);
        }
    }
}
