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
    private List<HeatCubeInfo> infos = new List<HeatCubeInfo>();
    private List<int> values = new List<int>();

    private int temp = 0;
    public bool Visible = false;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnGrid();
    }

    public GameObject GetCube()
    {
        return item;
    }

    public List<HeatCubeInfo> GetInfos()
    {
        return infos;
    }

    public List<int> GetScores()
    {
        values.Clear();
        foreach(HeatCubeInfo i in infos)
            values.Add(i.GetScore());
        return values;
    }

    public List<int> GetDeaths()
    {
        values.Clear();
        foreach(HeatCubeInfo i in infos)
            values.Add(i.GetDeaths());
        return values;
    }

    public List<int> GetItems()
    {
        values.Clear();
        foreach(HeatCubeInfo i in infos)
            values.Add(i.GetItems());
        return values;
    }

    public List<int> GetAiScore()
    {
        values.Clear();
        foreach(HeatCubeInfo i in infos)
            values.Add(i.GetAIScore());
        return values;
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

    public void SetItems(List<int> values)
    {
        for(int j = 0; j < cubes.Count; j++)
            cubes[j].GetComponent<HeatCubeInfo>().SetItems(values[j]);
    }

    public void SetAIScore(List<int> values)
    {
        for(int j = 0; j < cubes.Count; j++)
            cubes[j].GetComponent<HeatCubeInfo>().SetAIScore(values[j]);
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

    public void ResetItems()
    {
        for(int j = 0; j < cubes.Count; j++)
            cubes[j].GetComponent<HeatCubeInfo>().ResetItems();
    }

    public void ResetAIScore()
    {
        for(int j = 0; j < cubes.Count; j++)
            cubes[j].GetComponent<HeatCubeInfo>().ResetAIScore();
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
        if (Visible)
            clone.GetComponent<MeshRenderer>().enabled = true; 
        cubes.Add(clone);
        infos.Add(clone.GetComponent<HeatCubeInfo>());
    }
}
