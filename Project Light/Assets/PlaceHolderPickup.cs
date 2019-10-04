using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolderPickup : MonoBehaviour
{
    private bool triggered;
    public bool PickedUp {get; set;}
    public Transform onhand;
    public Transform parent;
    
    void Start()
    {
        PickedUp = false;
    }

    void Update()
    {
        if (triggered)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(PickedUp)
                    ReleaseItem();
                else
                    HoldItem();
            }
        }
    }

    private void HoldItem()
    {
        PickedUp = true;
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = onhand.position;
        this.transform.parent = parent;
    }

    private void ReleaseItem()
    {
        PickedUp = false;
        //this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Lootcube")
            triggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Lootcube")
            triggered = false;
    }
}
