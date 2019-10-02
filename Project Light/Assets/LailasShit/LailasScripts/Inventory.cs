using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
    public List<string> ItemsCarried;


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
            ItemsCarried.Add(item.name);
            if (onItemChangeCallBack != null)
            {
                onItemChangeCallBack.Invoke();
            }
        }
        return true;
    }
    public void Remove(Item item)
    {
        items.Remove(item);
        ItemsCarried.Remove(item.name);
        if (onItemChangeCallBack != null)
            onItemChangeCallBack.Invoke();
    }

    public void Crafting()
    {
        if (ItemsCarried.Contains("Book") & ItemsCarried.Contains("Money"))
        {
        }
    }
}
