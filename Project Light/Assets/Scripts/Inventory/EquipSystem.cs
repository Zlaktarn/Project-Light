using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSystem : MonoBehaviour
{
    public List<GameObject> equippable;

    private int equipSlot = 1;

    void Update()
    {
        EquipSlot();
        Equipped();
    }

    private void EquipSlot()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            equipSlot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            equipSlot = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            equipSlot = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            equipSlot = 4;
    }

    private void Equipped()
    {
        equippable[0] = null;
        if (equipSlot == 2)
        {
            equippable[1].SetActive(true);
        }
        else
            equippable[1].SetActive(false);

        if (equipSlot == 3)
            equippable[2].SetActive(true);
        else
            equippable[2].SetActive(false);

        if (equipSlot == 4)
            equippable[3].SetActive(true);
        else
            equippable[3].SetActive(false);
    }
}
