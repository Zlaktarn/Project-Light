using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenDegeneration : MonoBehaviour
{
    private GameObject player;
    private MovementScript playerScript;
    private float minHealth = 0f;
    private int degen = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<MovementScript>();
    }

    void Update()
    {
        if(playerScript.health > 100)
            playerScript.health = 100;

        if(playerScript.health > minHealth)
        {
            playerScript.health -= degen * Time.deltaTime;
            print("Oxygen: " + (int)playerScript.health);
        }
            

        if(playerScript.health < 0)
            playerScript.health = 0;
    }
}
