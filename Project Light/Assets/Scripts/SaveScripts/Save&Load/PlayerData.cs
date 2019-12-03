using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public float health;
    public float[] position;
    public int Pamount;
    public bool savedSpawn;
    public GameObject savedSpawnedSeed;

    public PlayerData(MovementScript player)
    {
        health = player.health;

        position = new float[3];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
    //public PlayerData(PlantManager pm)
    //{
    //    Pamount = pm.currentPlanted;
    //}
    //public PlayerData (PlantSeed ps)
    //{
    //    savedSpawn = ps.spawned;
    //    savedSpawnedSeed = ps.spawnedSeed;
    //}
}
