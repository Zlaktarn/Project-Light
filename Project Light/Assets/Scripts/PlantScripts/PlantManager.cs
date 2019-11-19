using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public PlantSeed[] seeds;
    private int currentPlanted = 0;
    public int maxPlanted = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < seeds.Length; i++)
            if(seeds[i].spawned)
                currentPlanted += 1;

        if(currentPlanted >= maxPlanted)
            currentPlanted = maxPlanted;
    }
}
