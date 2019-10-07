using UnityEngine;

public class ItemPickUpp : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;
    [SerializeField] KeyCode ItemPickUp = KeyCode.E;

    private bool isInRange;

    private void Update()
    {
        if(isInRange && Input.GetKeyDown(ItemPickUp))
        {
            inventory.AddItem(item);
            Destroy(gameObject);
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
