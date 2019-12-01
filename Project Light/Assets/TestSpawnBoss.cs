using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnBoss : MonoBehaviour
{
    public EnemyManager em;

    void Start()
    {
        em.SpawnBossAI();
    }
}
