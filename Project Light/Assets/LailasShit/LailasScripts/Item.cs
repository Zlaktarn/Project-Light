using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
}
