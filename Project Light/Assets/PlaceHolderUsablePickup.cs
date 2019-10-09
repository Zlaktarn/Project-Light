using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolderUsablePickup : MonoBehaviour
{
    private bool triggered;
    private bool otherTriggered;
    public bool PickedUp {get; set;}
    public string OtherTag;
    private Vector3 onhand = new Vector3(-0.23f, -0.15f, 0.46f);
    private Transform parent;
    private GameObject weapon;
    private LightAdjuster la;
    private Rigidbody rb;
    private int held = 0;
    
    void Start()
    {
        PickedUp = false;
        rb = GetComponent<Rigidbody>();
        parent = GameObject.Find("Hand").transform;
        weapon = GameObject.Find("Crossbow");
        la = GetComponent<LightAdjuster>();
    }

    void Update()
    {
        if (triggered)
        {
            if(IsOtherForward())
                otherTriggered = true;
            else
                otherTriggered = false;

            if (Input.GetKeyDown(KeyCode.E) && !PickedUp && !la.isPlanted)
            {
                HoldItem();
            }
            else if(Input.GetKeyDown(KeyCode.E) && PickedUp && !la.isPlanted)
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
        if (!otherTriggered)
        {
            PickedUp = false;
            this.transform.parent = null;
            rb.useGravity = true;
            rb.isKinematic = false;
            weapon.SetActive(true); 
        }
        else
        {
            PickedUp = false;
            Destroy(gameObject);
            weapon.SetActive(true);
        }
    }

    private bool IsOtherForward()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if(Physics.SphereCast(ray, 0.5f, out hit, 5.0f))
            if(hit.collider.tag == OtherTag)
                return true;
        return false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Lootcube")
            triggered = true;

        if(other.gameObject.tag == OtherTag)
            otherTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Lootcube")
            triggered = false;

        if(other.gameObject.tag == OtherTag)
            otherTriggered = false;
    }
}
