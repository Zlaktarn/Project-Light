using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHealthAdjuster : MonoBehaviour
{
    // Health Variables
    private GameObject player;
    private bool isInside;
    private MovementScript playerScript;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<MovementScript>();        
    }

    void Update()
    {
        HealthAdjuster();
    }

    void HealthAdjuster()
    {
        if (isInside)
            if(playerScript.health < 100)
            {
                playerScript.health += Time.deltaTime;
                Debug.Log("Hp: " + playerScript.health);
            }
        else
            if(playerScript.health > 0)
            {
                playerScript.health -= Time.deltaTime;
                Debug.Log("Hp: " + playerScript.health);
            }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            isInside = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            isInside = false;
    }
}
