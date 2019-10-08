using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltScript : MonoBehaviour
{
    Rigidbody myBody;
    private float lifeTimer = 6f;

    private float timer;
    private bool hitSomething = false;

    [SerializeField] float damage = 10f;

    private void Start()
    {
        myBody = GetComponent<Rigidbody>();
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

            if (collision.collider.tag == "Enemy")
                print("Hit!");

        }
    }

    private void Stick()
    {
        myBody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
