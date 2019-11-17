using UnityEngine;
using System;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;


    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;
    public event Action<BaseItemSlot> OnBeginDragE;
    public event Action<BaseItemSlot> OnEndDragE;
    public event Action<BaseItemSlot> OnDragE;
    public event Action<BaseItemSlot> OnDropE;

    void Start()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            equipmentSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            equipmentSlots[i].OnRightClickEvent += OnRightClickEvent;
            equipmentSlots[i].OnBeginDragE += OnBeginDragE;
            equipmentSlots[i].OnEndDragE += OnEndDragE;
            equipmentSlots[i].OnDragE += OnDragE;
            equipmentSlots[i].OnDropE += OnDropE;
        }
    }

    private void OnValidate()
    {

        equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();

    }

    public bool AddItem(EquippableItem item, out EquippableItem previousItem)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if(equipmentSlots[i].EquipmentType == item.EquipmentType)
            {
                previousItem = (EquippableItem)equipmentSlots[i].item;
                equipmentSlots[i].item = item;
                return true;
            }
        }
        previousItem = null;
        return false;
    }
    public bool RemoveItem(EquippableItem item)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].item == item)
            {
                equipmentSlots[i].item = null;
                return true;
            }
        }
        return false;
    }

}

