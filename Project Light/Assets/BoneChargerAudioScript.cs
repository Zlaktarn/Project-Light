using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneChargerAudioScript : MonoBehaviour
{
    public AudioClip mediumBoiWalk;
    public AudioClip mediumBoiCharge;

    //public AudioSource mediumBoiWalkAudio;
    //public AudioSource mediumBoiChargeAudio;

    private bool[] charging;

    private bool[] hostile;


    private GameObject[] BoneChargers;
    private AIChargeAttack[] BoneScripts;
    private AudioSource[] AudioSources;

    void Start()
    {
        //mediumBoiWalkAudio.clip = mediumBoiWalk;
        //mediumBoiChargeAudio.clip = mediumBoiCharge;
        //mediumBoiChargeAudio.loop = true;
        //mediumBoiWalkAudio.loop = true;


        BoneChargers = GameObject.FindGameObjectsWithTag("BoneCharger");


        for (int i = 0; i < BoneChargers.Length; i++)
        {
            BoneScripts[i] = BoneChargers[i].GetComponent<AIChargeAttack>();
        }

        for (int i = 0; i > hostile.Length; i++)
        {
            hostile[i] = false;
        }
    }

    void Update()
    {
        for (int i = 0; i > BoneChargers.Length; i++)
        {
            charging[i] = BoneScripts[i].attacking;
        }

        for (int i = 0; i > BoneChargers.Length; i++)
        {
            if (charging[i] && hostile[i])
            {
                mediumBoiChargeAudio.Play();
                mediumBoiWalkAudio.Stop();
                hostile[i] = false;

            }
            else if (!charging[i] && !hostile[i])
            {
                mediumBoiWalkAudio.Play();
                mediumBoiChargeAudio.Stop();
                hostile[i] = true;
            }
            else
            {
                Debug.Log("Something went wrong here!");
            }
        }
        
    }
}
