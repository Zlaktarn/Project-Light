using UnityEngine;

public class ItemPickUpp : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] Inventory inventory;
    //[SerializeField] SpriteRenderer spriteRender;
    [SerializeField] KeyCode ItemPickUp = KeyCode.E;

    private bool isInRange;

    private void Start()
    {
        //spriteRender.sprite = item.icon;
        //spriteRender.enabled = false;
    }

    private void Update()
    {
        if(isInRange && Input.GetKeyDown(ItemPickUp))
        {
            inventory.AddItem(Instantiate(item));
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isInRange = true;
        //spriteRender.enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isInRange = false;
        //spriteRender.enabled = false;
    }
}
