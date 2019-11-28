using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSystem : MonoBehaviour
{
    public List<GameObject> equippable;
    Inventory ammo;

    private int equipSlot = 1;

    private void Start()
    {
        ammo = gameObject.GetComponent<Inventory>();
        equippable[0] = null;

    }

    void Update()
    {
        EquipSlot();
        Equipped();
    }

    private void EquipSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || PlaceHolderPickup.PickedUp)
            equipSlot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2) && ammo.crossbow > 0 && !PlaceHolderPickup.PickedUp)
            equipSlot = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3) && ammo.gun > 0 && !PlaceHolderPickup.PickedUp)
            equipSlot = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4) && ammo.rifle > 0 && !PlaceHolderPickup.PickedUp)
            equipSlot = 4;
    }

    private void Equipped()
    {

        if (equipSlot == 2)
        {
            equippable[1].SetActive(true);
        }
        else
            equippable[1].SetActive(false);

        if (equipSlot == 3)
        {
            equippable[2].SetActive(true);
        }
        else
            equippable[2].SetActive(false);

        if (equipSlot == 4)
            equippable[3].SetActive(true);
        else
            equippable[3].SetActive(false);

    }
}
