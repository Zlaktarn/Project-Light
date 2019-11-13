using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public float impactForce = 30f;

    public Transform transform;
    Animator anim;
    public float animSpeed;

    private float nextTimeToFire = 0f;
    public float fireRate = 10;

    //public ParticleSystem muzzleFlash;
    //public GameObject impactEffect;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = animSpeed;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Shoot();
            Debug.Log("SHOT FIRED");
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        //muzzleFlash.Play();
        anim.SetTrigger("RifleShoot");
        if(Physics.Raycast(transform.position, transform.up, out hit, range))
        {
            Debug.Log(hit.transform.name);

            TargetScript target = hit.transform.GetComponent<TargetScript>();

            if(target != null)
            {
                target.TakeDamage(damage);
            }
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impactGO, 1f);
        }
    }
}
