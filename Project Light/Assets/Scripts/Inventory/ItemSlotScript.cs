using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotScript : MonoBehaviour
{
    [SerializeField] Image image;
    private ItemScript _item;
    public ItemScript item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item == null)
                image.enabled = false;
            else
            {
                image.sprite = _item.icon;
                image.enabled = true;
            }
        }
    }

    private void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
}
