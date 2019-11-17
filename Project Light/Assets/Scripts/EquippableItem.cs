using UnityEngine;

public enum EquipmentType
{
    Gun,
    Bow,
    Amulet,
    Armor,
    Gold,
}

[CreateAssetMenu]
public class EquippableItem : Item
{
    public int Damage, Ammo, Defense;
    [Space]
    public float DamagePercent, DefensePercent;
    [Space]
    public EquipmentType EquipmentType;

    public void Equip(InventoryManager c)
    {

    }
    public override Item GetCopy()
    {
        return Instantiate(this);
    }
    public override void Destroy()
    {
        Destroy(this);
    }
}
