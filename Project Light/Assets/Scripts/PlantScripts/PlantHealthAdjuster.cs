using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHealthAdjuster : MonoBehaviour
{
    // Health Variables
    private GameObject player;
    private MovementScript playerScript;
    public int regen = 2;
    private bool Enabled = false;

    // Temp Variables - should be added to player class
    private float maxHealth = 100f;
    private float minHealth = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<MovementScript>();
    }

    void Update()
    {
        if (Enabled)
        {
            if(playerScript.health <= maxHealth)
                playerScript.health += regen * Time.deltaTime;

            if(playerScript.health > maxHealth)
                playerScript.health = maxHealth;
        }
    }

    // Does not work when you leave one plants area but is still
    // inside another ones - needs fixing
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Enabled = true;
        } 

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Enabled = false;
        }
    }
}
