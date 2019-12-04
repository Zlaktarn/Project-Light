using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatCubeInfo : MonoBehaviour
{
    public int score = 0;
    public int deaths = 0;
    private int colorScore = 0;
    private int maxScore = 15;
    private Material mat;
    private GameObject player;
    private MovementScript playerScript;
    public Color startColor;
    public Color endColor;
    private bool once = false;
    public bool On = true;
    public bool Triggered = false;

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
        if (On)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerScript = player.GetComponent<MovementScript>(); 
        }
        mat = GetComponent<Renderer>().material;
        mat.color = startColor;
    }

    void Update()
    {
        if (!once && On)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerScript = player.GetComponent<MovementScript>();
            once = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AddScore(1);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
            if(playerScript != null)
                if(playerScript.health <= 0)
                    AddDeaths(15);

        if(other.tag == "Player")
            Triggered = true;
    }
}
