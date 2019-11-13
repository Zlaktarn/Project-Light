using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    private GameObject[] enemies;
    public GameObject slowAI;
    public GameObject fastAI;
    public GameObject bossAI;
    private GameObject spawnedAI;

    private float currentAmount;
    public float maxAmount;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public GameObject[] GetEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies;
    }

    public void SpawnSlowAI(Vector3 position)
    {
        Instantiate(slowAI, position, Quaternion.identity);
    }

    public void SpawnFastAI(Vector3 position)
    {
        Instantiate(fastAI, position, Quaternion.identity);
    }

    public void SpawnBossAI(Vector3 position)
    {
        Instantiate(bossAI, position, Quaternion.identity);
    }

    public float GetCurrentAmount()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        currentAmount = enemies.Length;
        return currentAmount;
    }

    public float GetMaxAmount()
    {
        return maxAmount;
    }

    public void SetMaxAmount(float amount)
    {
        maxAmount = amount;
    }

    public void SpawnMinions()
    {
        Debug.Log("Not Implemented!");
    }

    public void SpawnMinionsInRadius(Vector3 position, float radius, int amount)
    {
        for(int i = 0; i < amount - 1; i++)
        {
            var temp = Random.insideUnitSphere * radius + position;
            temp.y = 6f;
            spawnedAI = Instantiate(slowAI, temp, Random.rotation);
            spawnedAI.GetComponent<NavMeshAgent>().Warp(temp);
            spawnedAI.SetActive(true);
        }
    }
}
