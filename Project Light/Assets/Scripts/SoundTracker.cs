using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTracker : MonoBehaviour
{

    public int speed; //player speed, differs if player is walking, running, or crouch walking.

    //public AudioClip[] concreteJump = new AudioClip[3];//3 different types of sounds when landing
    //public AudioClip[] gravelJump = new AudioClip[3];
    //public AudioClip[] woodJump = new AudioClip[3];
    public AudioClip grassWalk;
    public AudioClip grassJump;


    //public AudioClip[] concreteSteps = new AudioClip[3];// [0] = crouch, [1] = walk, [2] = sprint
    //public AudioClip[] gravelSteps = new AudioClip[3];
    //public AudioClip[] woodSteps = new AudioClip[3];

    public int floorTypes; //0 = concrete, 1 = gravel, 2 = wood;

    public AudioSource jumpSource;
    public AudioSource stepsSource;

    public bool[] WASD = new bool[4];
    public bool walking = false;
    

    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            WASD[i] = false;
        }

        jumpSource.spatialBlend = 1;
        stepsSource.spatialBlend = 1;
        floorTypes = 0;

        jumpSource.volume = 0.5f;

        jumpSource.clip = grassJump;
        stepsSource.clip = grassWalk;
    }
    
    //void FloorUpdater()
    //{
    //    GetSpeed();
    //    if (floorTypes == 1)
    //    {
    //        stepsSource.clip = concreteSteps[speed];
    //        jumpSource.clip = concreteJump[UnityEngine.Random.Range(0, 3)];
    //        print(floorTypes.ToString());
    //    }
    //    else if (floorTypes == 2)
    //    {
    //        stepsSource.clip = gravelSteps[speed];
    //        jumpSource.clip = gravelJump[UnityEngine.Random.Range(0, 3)];
    //        print(floorTypes.ToString());
    //    }
    //    else if (floorTypes == 3)
    //    {
    //        stepsSource.clip = woodSteps[speed];
    //        jumpSource.clip = woodJump[UnityEngine.Random.Range(0, 3)];
    //        print(floorTypes.ToString());
    //    }
    //    else
    //    {
    //        print("Something's wrong!");
    //    }
    //}
    
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
        if (Input.GetKeyDown(KeyCode.W))
        {
            WASD[0] = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            WASD[0] = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            WASD[1] = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            WASD[1] = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            WASD[2] = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            WASD[2] = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            WASD[3] = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            WASD[3] = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !jumpSource.isPlaying) //JUMPING CHECK
        {
            jumpSource.PlayDelayed(0.7f);
        }

        if (Input.GetKeyDown(KeyCode.W) && !stepsSource.isPlaying) // WALKING CHECKS
        {
            stepsSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.A) && !stepsSource.isPlaying) //-||-
        {
            stepsSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.S) && !stepsSource.isPlaying)  //-||-
        {
            stepsSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.D) && !stepsSource.isPlaying) //-||-
        {
            stepsSource.Play();
        }

        if (WASD[0] == false && WASD[1] == false && WASD[2] == false && WASD[3] == false) //CHECK IF NOT WALKING
        {
            stepsSource.Stop();
        }

    }
}
