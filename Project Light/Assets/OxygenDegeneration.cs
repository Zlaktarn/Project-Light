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
        InvokeRepeating("DegenHealth", 0f, 1f);
    }

    private void DegenHealth()
    {
        if(playerScript.health > minHealth)
        {
            playerScript.health -= degen;
            print("Oxygen: " + playerScript.health);
        }
    }
}
