
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public float health = 100f;
    public BoltScript bolt;

    private void Update()
    {
        if (health <= 0f)
        {
            Die();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        //if(health <= 0f)
        //{
        //    Die();
        //}
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bolt")
            health -= bolt.damage;
    }
}
