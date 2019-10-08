using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeed : MonoBehaviour
{
    public GameObject seed;
    private GameObject spawnedSeed;
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
            if (Input.GetKeyDown(KeyCode.E) && !spawned)
            {
                spawnedSeed = (GameObject)Instantiate(seed, transform.localPosition + new Vector3(0, 0.64f, 0), Quaternion.identity);
                spawnedSeed.GetComponent<LightAdjuster>().isPlanted = true;
                //spawnedSeed.GetComponent<Rigidbody>().isKinematic = true;
                //spawnedSeed.GetComponent<Rigidbody>().useGravity = false;
                spawned = true;
            } 
        }
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
