using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    public Item item;
    bool Triggered = false;
    Color oldColor = Color.white;
    Renderer render;

    [SerializeField] public int itemID;

    private void Start()
    {
        render = GetComponent<Renderer>();
        render.material.color = oldColor;
    }

    void Update()
    {
        if (Triggered)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Interact();
                render.material.color = Color.green;
            }
            else
                render.material.color = Color.yellow;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
        {
            oldColor = render.material.color;
            render.material.color = Color.yellow;
            Triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        render.material.color = oldColor;

        Triggered = false;
    }
    public void Interact()
    {
        Debug.Log("Picking up" + item.name);
        bool wasPickeUp = Inventory.instance.Add(item);

        if (wasPickeUp)
            Destroy(gameObject);
    }
}
