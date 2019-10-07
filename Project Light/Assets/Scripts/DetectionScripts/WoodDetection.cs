using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDetection : MonoBehaviour
{
    private SoundTracker soundTracker;

    public static int thisType = 3;    

    void Start()
    {

    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            soundTracker = GameObject.Find("AudioObject").GetComponent<SoundTracker>();
            soundTracker.floorTypes = thisType;
            print("setting wood");
        }
    }


    void Update()
    {

    }
}
