using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenDegeneration : MonoBehaviour
{
    private GameObject player;
    private MovementScript playerScript;
    private float minOxygen = 0f;
    public int degen = 1;
    public bool on = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<MovementScript>();
    }

    void Update()
    {
        if (on)
        {
            if (playerScript.Oxygen > 100)
                playerScript.Oxygen = 100;

            if (playerScript.Oxygen > minOxygen)
            {
                playerScript.Oxygen -= degen * Time.deltaTime;
            }


            if (playerScript.Oxygen < 0)
                playerScript.Oxygen = 0;

            if (playerScript.Oxygen <= 0)
            {
                playerScript.health -= degen * Time.deltaTime;
                print("Health: " + (int)playerScript.health);
            } 
        }
    }
}
