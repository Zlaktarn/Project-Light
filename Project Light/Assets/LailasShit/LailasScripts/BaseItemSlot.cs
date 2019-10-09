using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] protected Image image;
    [SerializeField] protected Text AmountText;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;

    protected Color normalColor = Color.white;
    protected Color disabledColor = new Color(1, 1, 1, 0);


    private Item _item;

    public Item item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item == null)
            {
                image.color = disabledColor;
            }
            else
            {
                image.sprite = _item.icon;
                image.color = normalColor;
            }
        }
    }
    private int _amount;
    public int Amount
    {
        get { return _amount; }
        set
        {
            _amount = value;
            //AmountText.enabled = _item != null && _item.MaximumStacks > 1 && _amount > 1;
            //if (AmountText.enabled) AmountText.text = _amount.ToString();
        }
    }


    public virtual bool CanReceiveItem(Item item)
    {
        return false;
    }
    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();

        if (AmountText == null)
            AmountText = GetComponentInChildren<Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null)
                OnRightClickEvent(this);

        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterEvent != null)
        {
            OnPointerEnterEvent(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitEvent != null)
            OnPointerExitEvent(this);
    }
}
