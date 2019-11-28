using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChargeLeft : MonoBehaviour
{
    private GameObject player;
    private MovementScript playerScript;
    private Vector3 knockBackDir;
    private bool checkOnce;
    public bool attacking = true;
    public float damage = 10f;
    public float force;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<MovementScript>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
            if(attacking)
                if (checkOnce)
                {
                    playerScript.health -= damage;
                    Vector3 newPos = this.transform.position + (this.transform.right * 2);
                    knockBackDir = player.transform.position - newPos;
                    player.GetComponent<ImpactReceiver>().AddImpact(knockBackDir, force);
                    Debug.Log("Health: " + (int)playerScript.health);
                    checkOnce = false;
                }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "Player")
            checkOnce = true;
    }
}
