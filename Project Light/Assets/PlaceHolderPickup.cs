using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolderPickup : MonoBehaviour
{
    private bool triggered;
    public bool PickedUp {get; set;}
    private Vector3 onhand = new Vector3(-0.23f, -0.15f, 0.46f);
    public Transform parent;
    public GameObject weapon;
    private Rigidbody rb;
    private int held = 0;
    
    void Start()
    {
        PickedUp = false;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (triggered)
        {
            if (Input.GetKeyDown(KeyCode.E) && !PickedUp)
            {
                HoldItem();
            }
            else if(Input.GetKeyDown(KeyCode.E) && PickedUp)
            {
                ReleaseItem();
            }
        }
    }

    private void HoldItem()
    {
        weapon.SetActive(false);
        PickedUp = true;
        rb.useGravity = false;
        rb.isKinematic = true;
        this.transform.parent = parent;
        this.transform.localPosition = onhand;
        this.transform.localRotation = parent.localRotation;
    }

    private void ReleaseItem()
    {
        PickedUp = false;
        this.transform.parent = null;
        rb.useGravity = true;
        rb.isKinematic = false;
        weapon.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
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
