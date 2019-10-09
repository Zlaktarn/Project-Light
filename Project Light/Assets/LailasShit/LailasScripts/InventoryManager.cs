using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour //Charachter clas
{
    
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] ItemTooltip itemToolTip;
    [SerializeField] Image DraggableItem;

    private BaseItemSlot draggedSlot;



    private void OnValidate()
    {
        if (itemToolTip == null)
        {
            itemToolTip = FindObjectOfType<ItemTooltip>();
        }
    }

    void Awake()
    {
        inventory.OnRightClickEvent += Equip;
        equipmentPanel.OnRightClickEvent += Unequip;

        inventory.OnPointerEnterEvent += ShowToolTip;
        equipmentPanel.OnPointerEnterEvent += ShowToolTip;

        inventory.OnPointerExitEvent += HideToolTip;
        equipmentPanel.OnPointerExitEvent += HideToolTip;

        inventory.OnBeginDragE += BeginDrag;
        equipmentPanel.OnBeginDragE += BeginDrag;

        inventory.OnEndDragE += EndDrag;
        equipmentPanel.OnEndDragE += EndDrag;

        inventory.OnDragE += Drag;
        equipmentPanel.OnDragE += Drag;

        inventory.OnDropE += Drop;
        equipmentPanel.OnDropE += Drop;

    }

    private void Equip(BaseItemSlot itemSlots)
    {
        EquippableItem equippableItems = itemSlots.item as EquippableItem;
        if(equippableItems != null)
        {
            Equip(equippableItems);
        }
    }

    public void Unequip(BaseItemSlot itemSlots)
    {
        EquippableItem equippableItems = itemSlots.item as EquippableItem;
        if (equippableItems != null)
        {
            Unequip(equippableItems);
        }
    }

    private void ShowToolTip(BaseItemSlot itemSlots)
    {
        EquippableItem equippableItems = itemSlots.item as EquippableItem;
        if (equippableItems != null)
        {
            itemToolTip.ShowToolTip(equippableItems);
        }
    }
    private void HideToolTip(BaseItemSlot itemSlots)
    {
        itemToolTip.HideToolTip();
    }

    private void BeginDrag(BaseItemSlot itemSlots)
    {
        if(itemSlots.item != null)
        {
            draggedSlot = itemSlots;
            DraggableItem.sprite = itemSlots.item.icon;
            DraggableItem.transform.position = Input.mousePosition;
            DraggableItem.enabled = true;
        }
    }
    private void EndDrag(BaseItemSlot itemSlots)
    {
        draggedSlot = null;
        DraggableItem.enabled = false;
    }
    private void Drag(BaseItemSlot itemSlots)
    {

            DraggableItem.transform.position = Input.mousePosition;

    }
    private void Drop(BaseItemSlot dropItemSlot)
    {
        if (draggedSlot == null) return;


        Item draggedItem = draggedSlot.item;
        draggedSlot.item = dropItemSlot.item;
        dropItemSlot.item = draggedItem;
        int draggedItemAmount = draggedSlot.Amount;

        draggedSlot.Amount = dropItemSlot.Amount;



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
