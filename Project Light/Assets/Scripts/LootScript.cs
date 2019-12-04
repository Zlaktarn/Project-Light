using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    bool Triggered = false;
    public bool pickedUp = false;
    Color startColor = Color.white;
    Color oldColor;
    Renderer render;

    public string itemName;
    string name;
    Inventory inventory;
    GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Inventory");
        inventory = player.GetComponent<Inventory>();
        render = GetComponent<Renderer>();
        oldColor = startColor;
        render.material.color = oldColor;
    }

    void Update()
    {
        if (Triggered)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Interact();
                render.material.color = Color.green;
                AmmoAdd();
            }
            else
                render.material.color = Color.yellow;
        }
    }


    void AmmoAdd()
    {
        if(itemName == "cbAmmo")
        {
            inventory.cbAmmoTotal += Random.Range(5,8);
            Destroy(gameObject);
        }
        if (itemName == "rAmmo")
        {
            inventory.rAmmoTotal += Random.Range(5, 8);
            Destroy(gameObject);
        }
        if (itemName == "gAmmo")
        {
            inventory.gAmmoTotal += Random.Range(5, 8);
            Destroy(gameObject);
        }
        if (itemName == "Crossbow")
        {
            inventory.crossbow = 1;
            Destroy(gameObject);
        }
        if(itemName == "Gun")
        {
            inventory.gun = 1;
            Destroy(gameObject);
        }
        if (itemName == "Rifle")
        {
            inventory.rifle = 1;
            Destroy(gameObject);
        }
        if (itemName == "Water")
        {
            Inventory.water += 1;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Inventory"))
        {
            oldColor = render.material.color;
            render.material.color = Color.yellow;
            Triggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Inventory"))
        {
            render.material.color = startColor;

            Triggered = false;
        }
        
    }
    //public void Interact()
    //{
    //    Debug.Log("Picking up" + item.name);
    //    bool wasPickeUp = Inventory.instance.Add(item);

    //    if (wasPickeUp)
    //        Destroy(gameObject);
    //}
}
