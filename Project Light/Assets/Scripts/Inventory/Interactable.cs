using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 2f;
    //Transform player;


    //private void Update()
    //{
    //    float distance = Vector3.Distance(player.position, transform.position);
    //    if (distance <= radius)
    //        print("Interacted");

    //}

    //public virtual void Interact()
    //{
    //    print("Interacting");

    //    //if (other.tag == ("Player"))
    //    //    Destroy(gameObject);
    //}

    void Focused()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == ("Lootcube"))
            print("Hi Bitch!");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
