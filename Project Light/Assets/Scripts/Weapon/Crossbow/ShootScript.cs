using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootScript : MonoBehaviour
{
    public string weaponType;
    public Camera cam;
    public GameObject boltPrefab;
    public Transform boltSpawn;
    public Animator anim;
    [SerializeField] float shootForce;

    public Inventory ammo;
    private int remAmmo;

    bool isReloading = false;

    private int reloadTime = 1;

    [SerializeField] public int currentAmmo = -1;
    [SerializeField] int clipSize = 1;
    [SerializeField] float fireRate = 1;

    private float nextTimeToFire = 0f;

    public Text ammoText;

    private void Start()
    {
        currentAmmo = clipSize;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Shoot();
        Reload();
    }

    void Shoot()
    {
        if (isReloading)
            return;

        if (!isReloading && currentAmmo > 0)
            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextTimeToFire)
            {
                anim.SetBool("MakeShoot", true);
                GameObject gameObject = Instantiate(boltPrefab, boltSpawn.position, Quaternion.identity);
                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.velocity = cam.transform.forward * shootForce;
                currentAmmo--;
            }

        if (ammo.cbAmmoTotal > 0)
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload());
                return;
            }

        ammoText.text = currentAmmo.ToString() + " / " + ammo.cbAmmoTotal;
    }

    IEnumerator Reload()
    {
        remAmmo = currentAmmo;

        isReloading = true;
        print("reloading");

        yield return new WaitForSeconds(reloadTime);

        if (ammo.cbAmmoTotal + currentAmmo >= clipSize)
            currentAmmo = clipSize;
        else if ((ammo.cbAmmoTotal + currentAmmo) < clipSize)
            currentAmmo = ammo.cbAmmoTotal + remAmmo;

        ammo.cbAmmoTotal -= clipSize - remAmmo;

        isReloading = false;
    }
}