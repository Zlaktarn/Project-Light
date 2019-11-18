using UnityEngine;
using System.Collections;

public class BackupGun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;


    public Transform transform;

    public float fireRate;
    private float nextTimeToFire = 0f;

    private Animator anim;


    public AmmoScript ammo;
    public int clipSize = 6;
    public int currentAmmo = -1;
    public float reloadTime = 1f;
    private bool isReloading = false;


    private float animSpeed;

    //public ParticleSystem muzzleFlash;
    //public GameObject impactEffect;

    void Start()
    {
        currentAmmo = clipSize;

        anim = GetComponent<Animator>();
        animSpeed = 2.5f;
        anim.speed = animSpeed;
    }

    private void OnEnable()
    {
        isReloading = false;
    }

    void Update()
    {
        if (isReloading)
            return;

        if (!isReloading && currentAmmo > 0)
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }

        if (ammo.gAmmoTotal > 0)
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload());
                return;
            }
    }

    IEnumerator Reload()
    {
        int remAmmo = currentAmmo;

        isReloading = true;
        print("reloading");


        yield return new WaitForSeconds(reloadTime);

        if (ammo.gAmmoTotal + currentAmmo >= clipSize)
            currentAmmo = clipSize;
        else if ((ammo.gAmmoTotal + currentAmmo) < clipSize)
            currentAmmo = ammo.gAmmoTotal + remAmmo;

        ammo.gAmmoTotal -= clipSize - remAmmo;

        isReloading = false;
    }

    void Shoot()
    {

        RaycastHit hit;
        //muzzleFlash.Play();

        anim.SetTrigger("Shoot");
        if (Physics.Raycast(transform.position, transform.up, out hit, range))
        {
            Debug.Log(hit.transform.name);

            TargetScript target = hit.transform.GetComponent<TargetScript>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            //GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //Destroy(impactGO, 1f);
        }

        currentAmmo--;
    }
}
