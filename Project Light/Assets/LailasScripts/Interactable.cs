using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Item item;
    public float radius = 3f;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if(distance <= radius)
        {
            Debug.Log("Press LMB to pick up");
            if(Input.GetMouseButtonDown(0))
            {
                Interact();
            }
        }
        
    }
    public void Interact()
    {
        Debug.Log("Picking up" + item.name);
        bool wasPickeUp = Inventory.instance.Add(item);

        if(wasPickeUp)
        Destroy(gameObject);
    }
}
