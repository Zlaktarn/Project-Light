using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] Image image;
    [SerializeField] Text AmountText;
    [SerializeField] ItemTooltip toolTip;


    public event Action<Item> OnPointerEnterEvent;
    public event Action<Item> OnPointerExitEvent;
    public event Action<Item> OnRightClickEvent;

    private Item _item;

    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item == null)
            {
                image.enabled = false;
            }
            else
            {
                image.sprite = _item.icon;
                image.enabled = true;
            }
        }
    }
    //private int _amount;
    //public int Amount
    //{ get { return _amount; }
    //    set
    //    {
    //        _amount = value;
    //        AmountText.enabled = _item != null && _item.MaximumStack
    //    }
    //}
    

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if(item != null && OnRightClickEvent != null)
            {
                OnRightClickEvent(item);
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
        if(toolTip == null)
        {
            toolTip = FindObjectOfType<ItemTooltip>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item is EquippableItem)
        {
            toolTip.ShowToolTip((EquippableItem)item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.HideToolTip();
    }
}
