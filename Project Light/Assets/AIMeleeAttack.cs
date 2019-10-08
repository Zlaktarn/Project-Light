using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMeleeAttack : MonoBehaviour
{
    private GameObject player;
    private MovementScript playerScript;
    private bool checkOnce = true;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<MovementScript>();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            if (checkOnce)
            {
                playerScript.health -= 10;
                Debug.Log("Health: " + (int)playerScript.health); 
                checkOnce = false;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            checkOnce = true;
        }
    }
}
