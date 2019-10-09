using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : BaseItemSlot, IBeginDragHandler, IEndDragHandler, IDragHandler,IDropHandler
{
    public event Action<BaseItemSlot> OnBeginDragE;
    public event Action<BaseItemSlot> OnEndDragE;
    public event Action<BaseItemSlot> OnDragE;
    public event Action<BaseItemSlot> OnDropE;

    private Color dragColor = Color.white;

    public override bool CanReceiveItem(Item item)
    {
        return true;
    }


    //Events
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
            image.color = dragColor;

        if (OnBeginDragE != null)
            OnBeginDragE(this);
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item != null)
            image.color = normalColor;

        if (OnEndDragE != null)
            OnEndDragE(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
            image.color = dragColor;

        if (OnBeginDragE != null)
            OnBeginDragE(this);
    }

    public void OnDrop(PointerEventData eventData)
    {

    }
}
