using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatCubeInfo : MonoBehaviour
{
    public int score = 0;
    public int deaths = 0;
    private int colorScore = 0;
    private int maxScore = 30;
    private Material mat;
    public Color startColor;
    public Color endColor;

    public int GetScore()
    {
        return score;
    }

    public int GetDeaths()
    {
        return deaths;
    }

    public void ResetDeaths()
    {
        deaths = 0;
        mat.color = Color.Lerp(startColor, endColor, (float)deaths/maxScore);
    }

    public void SetDeaths(int i)
    {
        deaths = i;
        mat.color = Color.Lerp(startColor, endColor, (float)deaths/maxScore);
        if(deaths >= maxScore)
            deaths = maxScore;
    }

    public void AddDeaths(int i)
    {
        deaths += i;
        mat.color = Color.Lerp(startColor, endColor, (float)deaths/maxScore);
        if(deaths >= maxScore)
            deaths = maxScore;
    }

    public void ResetScore()
    {
        score = 0;
        mat.color = Color.Lerp(startColor, endColor, (float)score/maxScore);
    }

    public void SetScore(int i)
    {
        score = i;
        mat.color = Color.Lerp(startColor, endColor, (float)score/maxScore);
        if(score >= maxScore)
            score = maxScore;
    }

    public void AddScore(int i)
    {
        score += i;
        if(score >= maxScore)
            score = maxScore;
    }

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.color = startColor;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AddScore(1);
            Debug.Log(score);
        }
    }
}
