using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatCubeInfo : MonoBehaviour
{
    public int score = 0;

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int i)
    {
        score = i;
    }

    public void AddScore(int i)
    {
        score += i;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            score += 1;
            Debug.Log(score);
        }
    }
}
