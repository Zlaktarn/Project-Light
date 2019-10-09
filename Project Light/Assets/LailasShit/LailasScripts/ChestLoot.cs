using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLoot : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;
    [SerializeField] KeyCode ItemPickUp = KeyCode.E;

    private bool isInRange;

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(ItemPickUp))
        {
            if(item != null)
            {
                inventory.AddItem(Instantiate(item));
                item = null;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isInRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
    }
}
