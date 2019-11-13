using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltScript : MonoBehaviour
{
    Rigidbody myBody;
    private float lifeTimer = 2f;

    private float timer;
    private bool hitSomething = false;

    public float damage;
    TargetScript target;

    private void Start()
    {
        myBody = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(myBody.velocity);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTimer)
        {
            Destroy(gameObject);
        }

        if (!hitSomething)
        {
            transform.rotation = Quaternion.LookRotation(myBody.velocity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Bolt" && collision.collider.tag != "Player")
        {
            hitSomething = true;
            Stick();

            //if (collision.collider.tag == "Enemy")
            //    target.health -= damage;

        }
    }

    private void Stick()
    {
        myBody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
