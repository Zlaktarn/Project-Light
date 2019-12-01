using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolderPickup : MonoBehaviour
{
    private bool triggered;
    public static bool PickedUp {get; set;}
    private Vector3 onhand = new Vector3(-0.23f, -0.15f, 0.46f);
    public Transform parent;
    public Camera fpsCam;
    private GameObject item;
    private Rigidbody rb;
    private int held = 0;
    private int itemLayer = 1<<11;
    private int usableLayer = 1<<12;
    
    void Start()
    {
        PickedUp = false;
    }

    void Update()
    {

        if (triggered)
        {
            if (Input.GetKeyDown(KeyCode.E) && !PickedUp)
            {
                if (FindItem())
                {
                    HoldItem(); 
                }
            }
            else if(Input.GetKeyDown(KeyCode.E) && PickedUp)
            {
                if (rb != null)
                {
                    ReleaseItem(); 
                }
            }
        }
    }

    private void HoldItem()
    {
        PickedUp = true;
        rb.useGravity = false;
        rb.isKinematic = true;
        item.GetComponent<MeshCollider>().enabled = false;
        item.transform.parent = parent;
        item.transform.localPosition = onhand;
        item.transform.localRotation = parent.localRotation;
    }

    private void ReleaseItem()
    {
        if (!IsUsable())
        {
            PickedUp = false;
            item.transform.localPosition = new Vector3(0, 0.1f, 1.5f);
            item.transform.rotation = new Quaternion(0, 0, 0, 1);
            item.transform.parent = null;
            rb.useGravity = true;
            rb.isKinematic = false;
            item.GetComponent<MeshCollider>().enabled = true;
        }
    }

    private bool IsUsable()
    {
        RaycastHit hit;
        if(item.tag == "Usable" && Physics.SphereCast(fpsCam.transform.position, 0.5f, fpsCam.transform.forward, out hit, 4f, usableLayer))
            return true;
        return false;
    }

    private bool FindItem()
    {
        RaycastHit hit;
        if(Physics.SphereCast(fpsCam.transform.position, 2f, fpsCam.transform.forward, out hit, 8f, itemLayer))
        {
            rb = hit.transform.gameObject.GetComponent<Rigidbody>();
            item = hit.transform.gameObject;
            return true;
        }
        else
            rb = null;
        return false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Inventory")
            triggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Inventory")
            triggered = false;
    }
}
