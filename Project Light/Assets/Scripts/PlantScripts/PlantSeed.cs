using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeed : MonoBehaviour
{
    public GameObjectSearcher search;
    public PlantManager pm;
    Inventory inventory;
    public GameObject seed;
    private GameObject preSeed;
    public GameObject spawnedSeed;
    public GameObject parent;
    public Camera cam;
    private Rigidbody rb;
    public bool spawned = false;
    private bool triggered = false;
    public bool Test = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (IsLookingAtSpot())
        {
            if (IsSeedHeld())
            {
                Debug.Log("worked!");
                if (Input.GetKeyDown(KeyCode.E) && !spawned)
                {
                    spawnedSeed = (GameObject)Instantiate(seed, transform.position, Quaternion.identity);
                    spawnedSeed.GetComponent<LightAdjuster>().isPlanted = true;
                    Destroy(preSeed);
                    pm.AddCurrentPlanted(1);
                    PlaceHolderPickup.PickedUp = false;
                    spawned = true;
                }  
            }
        }

        if (Test && Input.GetKeyDown(KeyCode.E) && !spawned)
        {
            Debug.Log("hej");
            spawnedSeed = (GameObject)Instantiate(seed, transform.position, Quaternion.identity);
            spawnedSeed.GetComponent<LightAdjuster>().isPlanted = true;
            pm.AddCurrentPlanted(5);
            spawned = true;
        }
    }

    private bool IsSeedHeld()
    {
        preSeed = search.GetChildObject(parent.transform, "Usable");
        if(preSeed != null)
            return true;
        return false;
    }

    private bool IsLookingAtSpot()
    {
        if (cam != null)
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 4))
                if (hit.collider.gameObject == gameObject)
                    return true; 
        }
        return false;
    }
}
