using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour {

    public GameObject cameraTarget;  // Make an empty for your target
    public float speed; //speed of the rotation

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        OrbitAround();

    }

    void OrbitAround()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f);
    }
}
