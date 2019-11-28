using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    private GameObject[] enemies;
    public GameObject[] spawnPositions;
    public GameObject bossSpawnPosition;
    public GameObject slowAI;
    public GameObject fastAI;
    public GameObject bossAI;
    private GameObject spawnedAI;

    private float currentAmount;
    public float maxAmount;
    public float spawnInterval = 2f;
    public bool IsBossDead = false;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        InvokeRepeating("SpawnMinions", 1f, spawnInterval);
    }

    void Update()
    {
        if(currentAmount >= maxAmount)
            if(IsInvoking())
                CancelInvoke();
    }

    public GameObject[] GetEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies;
    }

    public GameObject SpawnSlowAI(Vector3 position)
    {
        var hej = Instantiate(slowAI, position, Quaternion.identity);
        return hej;
    }

    public GameObject SpawnFastAI(Vector3 position)
    {
        var hej = Instantiate(fastAI, position, Quaternion.identity);
        return hej;
    }

    public void SpawnBossAI()
    {
        var hej = Instantiate(bossAI, bossSpawnPosition.transform.position, Quaternion.identity);
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
        for(int i = 0; i < spawnPositions.Length; i++)
        {
            var rnd = Random.Range(0,2);
            if(rnd == 0)
            {
                SpawnSlowAI(spawnPositions[i].transform.position);
                currentAmount += 1;
            }
            else
            {
                SpawnFastAI(spawnPositions[i].transform.position);
                currentAmount += 1;
            }
        }
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
