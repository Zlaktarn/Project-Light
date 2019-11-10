using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChargeAttack : MonoBehaviour
{
    private GameObject player;
    private MovementScript playerScript;
    private bool checkOnce = true;
    public bool attacking = false;
    private Vector3 knockBackDir;
    public float force = 250;
    public float smallForce = 100f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<MovementScript>();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            if (attacking)
            {
                if (checkOnce)
                {
                    playerScript.health -= 10;
                    knockBackDir = player.transform.position - this.transform.position;
                    player.GetComponent<ImpactReceiver>().AddImpact(knockBackDir, force);
                    Debug.Log("Health: " + (int)playerScript.health);
                    checkOnce = false;
                } 
            }
            else
            {
                knockBackDir = player.transform.position - this.transform.position;
                player.GetComponent<ImpactReceiver>().AddImpact(knockBackDir, smallForce);
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
