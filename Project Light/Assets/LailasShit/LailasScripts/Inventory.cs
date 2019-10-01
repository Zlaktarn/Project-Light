using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region
    public static Inventory instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Dont work");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChange();

    public OnItemChange onItemChangeCallBack;


    public int space = 3;

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room");
                return false;
            }
            items.Add(item);
            if (onItemChangeCallBack != null)
                onItemChangeCallBack.Invoke();
        }
        return true;


    }
    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangeCallBack != null)
            onItemChangeCallBack.Invoke();
    }
}
