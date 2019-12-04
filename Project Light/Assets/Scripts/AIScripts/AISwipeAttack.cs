using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISwipeAttack : MonoBehaviour
{
    private float time = 0;
    public float duration = 2;
    public float distance = 2;
    public float shieldDuration = 0.2f;
    public float damage = 10f;
    public float force = 50f;
    private Vector3 knockBackDir;
    public bool attacking = true;
    private bool shield = true;
    private bool remove = false;
    private bool onlyOnce = false;
    private GameObject player;
    private MovementScript playerScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<MovementScript>();
    }

    void Update()
    {
        time += Time.deltaTime/duration;
        if(time >= shieldDuration)
            shield = false;

        if(time >= 1)
        {
            attacking = false;
        }

        if(time >= 1.1)
                remove = true;

        if(remove)
            Destroy(gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!shield && playerScript != null)
            {
                knockBackDir = player.transform.position - transform.position;
                player.GetComponent<ImpactReceiver>().AddImpact(knockBackDir, force); 
            }

            if (playerScript != null && !shield && !onlyOnce)
            {
                playerScript.health -= damage;
                Debug.Log("Health: " + (int)playerScript.health);
                onlyOnce = true;
            }
        }
    }
}
