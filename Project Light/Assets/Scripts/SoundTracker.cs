using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTracker : MonoBehaviour
{

    public int speed; //player speed, differs if player is walking, running, or crouch walking.

    public AudioClip[] concreteJump = new AudioClip[3];//3 different types of sounds when landing
    public AudioClip[] gravelJump = new AudioClip[3];
    public AudioClip[] woodJump = new AudioClip[3];


    public AudioClip[] concreteSteps = new AudioClip[3];// [0] = crouch, [1] = walk, [2] = sprint
    public AudioClip[] gravelSteps = new AudioClip[3];
    public AudioClip[] woodSteps = new AudioClip[3];

    public int floorTypes; //0 = concrete, 1 = gravel, 2 = wood;

    public AudioSource jumpSource;
    public AudioSource stepsSource;
    

    void Start()
    {
        jumpSource.spatialBlend = 1;
        stepsSource.spatialBlend = 1;
        floorTypes = 0;
    }
    
    void FloorUpdater()
    {
        GetSpeed();
        if (floorTypes == 1)
        {
            stepsSource.clip = concreteSteps[speed];
            jumpSource.clip = concreteJump[UnityEngine.Random.Range(0, 3)];
            print("concrete");
        }
        else if (floorTypes == 2)
        {
            stepsSource.clip = gravelSteps[speed];
            jumpSource.clip = gravelJump[UnityEngine.Random.Range(0, 3)];
            print("gravel");
        }
        else if (floorTypes == 3)
        {
            stepsSource.clip = woodSteps[speed];
            jumpSource.clip = woodJump[UnityEngine.Random.Range(0, 3)];
            print("wood");
        }
        else
        {
            print("Something's wrong!");
        }
    }
    
    void GetSpeed()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && speed != 0)
        {
            speed = 0;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && speed != 2)
        {
            speed = 2;
        }
        else
        {
            speed = 1;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FloorUpdater();
            jumpSource.PlayDelayed(0.5f);
        }


        if (Input.GetKeyDown(KeyCode.W) && !stepsSource.isPlaying)
        {
            FloorUpdater();
            stepsSource.loop = true;
            stepsSource.Play();
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            stepsSource.Stop();
        }

    }
}
