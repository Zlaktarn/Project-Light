using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public PlantSeed[] seeds;
    public EnemyManager em;
    public int currentPlanted = 0;
    public int maxPlanted = 5;
    private bool bossSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddCurrentPlanted(int amount)
    {
        currentPlanted += amount;
        if(currentPlanted >= maxPlanted)
            currentPlanted = maxPlanted;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPlanted >= maxPlanted && !bossSpawned)
        {
            em.SpawnBossAI();
            bossSpawned = true;
        }
            
    }
}
