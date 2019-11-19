using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeed : MonoBehaviour
{
    public GameObjectSearcher search;
    Inventory inventory;
    public GameObject seed;
    private GameObject preSeed;
    private GameObject spawnedSeed;
    public GameObject parent;
    private Rigidbody rb;
    private bool spawned = false;
    private bool triggered = false;

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
                    spawnedSeed = (GameObject)Instantiate(seed, transform.localPosition + new Vector3(0, 0, 0), Quaternion.identity);
                    spawnedSeed.GetComponent<LightAdjuster>().isPlanted = true;
                    Destroy(preSeed);
                    spawned = true;
                }  
            }
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
