using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHealthAdjuster : MonoBehaviour
{
    // Health Variables
    private GameObject player;
    private MovementScript playerScript;
    public int regen = 2;

    // Temp Variables - should be added to player class
    private float maxHealth = 100f;
    private float minHealth = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<MovementScript>();
    }

    // These are of the type invoke repeating
    // which may cause bugs and should possibly change to Coroutines
    private void RegenHealth()
    {
        if(playerScript.health <= maxHealth)
        {
            playerScript.health += regen;
            print("Oxygen: " + playerScript.health);
        }
    }

    // Does not work when you leave one plants area but is still
    // inside another ones - needs fixing
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !IsInvoking("RegenHealth"))
        {
            Debug.Log("Started regen");
            InvokeRepeating("RegenHealth", 0f, 1f);
        } 

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CancelInvoke("RegenHealth");
        }
    }
}
