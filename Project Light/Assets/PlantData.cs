using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantData : MonoBehaviour
{
    public PlantSeed[] plants;
    private int[] savedPlants;
    public float[] position;

    bool spawned1 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PlantData(PlantSeed plant)
    {
        for (int i = 0; i < plants.Length; i++)
        {
            if(plants[i].IsSpawned())
            {
                savedPlants[i] = 1;
            }
        } 

        position = new float[3];

        position[0] = plant.transform.position.x;
        position[1] = plant.transform.position.y;
        position[2] = plant.transform.position.z;
    }
}
