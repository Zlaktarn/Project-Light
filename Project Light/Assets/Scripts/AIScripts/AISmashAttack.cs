using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISmashAttack : MonoBehaviour
{
    public float force = 250;
    private float alpha;
    public float duration;
    private float alphaTime;
    private bool doDamage = false;
    private bool remove = false;
    public bool attacking = true;
    private Material mat;
    private GameObject player;
    private MovementScript playerScript;
    private Vector3 knockBackDir;
    public GameObject knockBackPos;

    void Awake()
    {
        alpha = 0f;
        alphaTime = 0f;
        //duration = 2f;
        mat = GetComponent<Renderer>().material;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<MovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Inside: " + attacking);
        if (!doDamage)
        {
            alphaTime += Time.deltaTime/duration;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, Mathf.Lerp(alpha, 1.0f, alphaTime));
            if(alphaTime >= 1f)
            {
                doDamage = true;
                attacking = false;
            }
        }

        if (doDamage)
        {
            alphaTime += Time.deltaTime;

            if(alphaTime >= 1.1f)
                remove = true;
        }

        if (remove)
            Destroy(gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (doDamage)
            {
                playerScript.health -= 20;
                knockBackDir = player.transform.position - knockBackPos.transform.position;
                player.GetComponent<ImpactReceiver>().AddImpact(knockBackDir, force);
                Debug.Log("Health: " + (int)playerScript.health);
                remove = true;
            } 
        }
    }
}
