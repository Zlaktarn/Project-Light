using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneChargerAudioScript : MonoBehaviour
{
    public AudioClip mediumBoiWalk;
    public AudioClip mediumBoiCharge;

    public AudioSource mediumBoiWalkAudio;
    public AudioSource mediumBoiChargeAudio;

    public bool charging;

    public bool hostile = false;

    private GameObject BoneCharger;
    private AIChargeAttack BoneScript;

    void Start()
    {
        mediumBoiWalkAudio.clip = mediumBoiWalk;
        mediumBoiChargeAudio.clip = mediumBoiCharge;
        mediumBoiChargeAudio.loop = true;
        mediumBoiWalkAudio.loop = true;

        BoneCharger = GameObject.FindWithTag("BoneCharger");
        BoneScript = BoneCharger.GetComponent<AIChargeAttack>();
    }
    
    void Update()
    {
        charging = BoneScript.attacking;

        if(charging && hostile)
        {
            mediumBoiChargeAudio.Play();
            mediumBoiWalkAudio.Stop();
            hostile = false;

        }
        else if(!charging && !hostile)
        {
            mediumBoiWalkAudio.Play();
            mediumBoiChargeAudio.Stop();
            hostile = true;
        }
        else
        {
            Debug.Log("Something went wrong here!");
        }
        

    }
}
