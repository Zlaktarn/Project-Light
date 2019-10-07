using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravelDetection : MonoBehaviour
{
    private SoundTracker soundTracker;

    public static int thisType = 2;

    void Start()
    {
        
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            soundTracker = GameObject.Find("AudioObject").GetComponent<SoundTracker>();
            soundTracker.floorTypes = thisType;
            print("setting gravel");
        }
    }

    void Update()
    {
        
    }
}
