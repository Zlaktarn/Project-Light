using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMap : MonoBehaviour
{
    public GameObject item;
    public int gridX;
    public int gridZ;
    public float gridSpacingOffset = 1f;
    public Vector3 gridOrigin = Vector3.zero;
    private List<GameObject> cubes = new List<GameObject>();

    private int temp = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnGrid();
    }

    public List<GameObject> GetCubes()
    {
        return cubes;
    }

    public void SetActivety(List<int> values)
    {
        for(int j = 0; j < cubes.Count; j++)
            cubes[j].GetComponent<HeatCubeInfo>().SetScore(values[j]);
    }

    public void SetDeaths(List<int> values)
    {
        for(int j = 0; j < cubes.Count; j++)
            cubes[j].GetComponent<HeatCubeInfo>().SetDeaths(values[j]);
    }

    public void ResetActivety()
    {
        for(int j = 0; j < cubes.Count; j++)
            cubes[j].GetComponent<HeatCubeInfo>().ResetScore();
    }

    public void ResetDeaths()
    {
        for(int j = 0; j < cubes.Count; j++)
            cubes[j].GetComponent<HeatCubeInfo>().ResetDeaths();
    }

    void SpawnGrid()
    {
        for(int x = 0; x < gridX; x++)
        {
            for(int z = 0; z < gridZ; z++)
            {
                Vector3 spawnPosition = new Vector3(x * gridSpacingOffset, 0, z * gridSpacingOffset) + gridOrigin;
                PickAndSpawn(spawnPosition, Quaternion.identity);
            }
        }
    }

    void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        GameObject clone = Instantiate(item, positionToSpawn, rotationToSpawn);
        cubes.Add(clone);
    }
}
