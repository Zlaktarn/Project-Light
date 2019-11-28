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
    private GameObject spawnedSeed;
    public GameObject parent;
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
        if (triggered)
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

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Lootcube")
            triggered = true;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Lootcube")
            triggered = false;
    }
}
