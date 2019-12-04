using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChargeAttack : MonoBehaviour
{
    private GameObject player;
    private MovementScript playerScript;
    private Vector3 knockBackDir;
    private bool checkOnce, checkOnce1;
    public bool attacking = true;
    public float damage = 10f;
    public float force = 100f;
    public float passiveForce = 15f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<MovementScript>();
    }

    public float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);
 
        if (dir > 0.0f) {
            return 1.0f;
        } else if (dir < 0.0f) {
            return -1.0f;
        } else {
            return 0.0f;
        }
    }  
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (attacking)
            {
                Vector3 temp = player.transform.position - this.transform.position;
                if(AngleDir(transform.forward, temp, transform.up) == -1)
                    temp = this.transform.position + (this.transform.right * 2);
                else if(AngleDir(transform.forward, temp, transform.up) == 1)
                    temp = this.transform.position + (-this.transform.right * 2);
                else if(AngleDir(transform.forward, temp, transform.up) == 0)
                    temp = this.transform.position + (-this.transform.right * 2);
                knockBackDir = player.transform.position - temp;
                player.GetComponent<ImpactReceiver>().AddImpact(knockBackDir, force);
            }
                if (checkOnce)
                {
                    playerScript.health -= damage;
                    Debug.Log("Health: " + (int)playerScript.health);
                    checkOnce = false;
                } 

                Vector3 temp2 = player.transform.position - this.transform.position;
                if(AngleDir(transform.forward, temp2, transform.up) == -1)
                    temp2 = this.transform.position + (this.transform.right * 2);
                else if(AngleDir(transform.forward, temp2, transform.up) == 1)
                    temp2 = this.transform.position + (-this.transform.right * 2);
                else if(AngleDir(transform.forward, temp2, transform.up) == 0)
                    temp2 = this.transform.position + (-this.transform.right * 2);
                knockBackDir = player.transform.position - temp2;
                player.GetComponent<ImpactReceiver>().AddImpact(knockBackDir, passiveForce);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.collider.tag == "Player")
            checkOnce = true;
    }
}
