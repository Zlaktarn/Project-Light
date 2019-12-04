using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{

    public float health;
    public float[] position;

    //public int Pamount;
    //public bool savedSpawn;
    //public GameObject savedSpawnedSeed;

    public int[] inv;

    public PlayerData(MovementScript player)
    {
        health = player.health;

        position = new float[3];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;


    }

    public PlayerData(Inventory inventory)
    {
        inv[0] = inventory.gAmmoTotal;
        inv[1] = inventory.rAmmoTotal;
        inv[2] = inventory.cbAmmoTotal;
        inv[3] = inventory.gun;
        inv[4] = inventory.rifle;
        inv[5] = inventory.crossbow;
        inv[6] = Inventory.water;
    }

    public PlayerData(PlantSeed ps)
    {

    }


    //public PlayerData(PlantManager pm)
    //{
    //    Pamount = pm.currentPlanted;
    //}
    //public PlayerData(PlantSeed ps)
    //{
    //    savedSpawn = ps.spawned;
    //    savedSpawnedSeed = ps.spawnedSeed;
    //}
}
