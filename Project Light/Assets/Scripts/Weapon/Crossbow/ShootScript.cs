﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{

    public Camera cam;
    public GameObject boltPrefab;
    public Transform boltSpawn;
    [SerializeField] float shootForce;

    private float timer;
    private float timeInterval = 2f;

    bool reloading = false;
    bool loaded = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && loaded)
        {
            GameObject gameObject = Instantiate(boltPrefab, boltSpawn.position, Quaternion.identity);
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.velocity = cam.transform.forward * shootForce;
            loaded = false;
        }

        Reloading();
    }

    private void Reloading()
    {
        if(Input.GetKeyDown(KeyCode.R) && !loaded && !reloading)
        {
            reloading = true;
            print("Reloading..");
        }

        if (reloading)
        {
            timer += Time.deltaTime;
            this.transform.Rotate(0, 0, 180 * Time.deltaTime, Space.Self);

            if (timer >= timeInterval)
            {
                print("Loaded!");
                timer = 0;
                loaded = true;
                reloading = false;
            }
        }
    }
}
