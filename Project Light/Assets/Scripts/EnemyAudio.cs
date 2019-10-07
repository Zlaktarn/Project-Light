using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public AudioClip ambience;

    public AudioSource SoundSource;

    void Start()
    {
        SoundSource.clip = ambience;
        SoundSource.loop = true;
        SoundSource.spatialBlend = 1;
        SoundSource.Play();
    }
    
    void Update()
    {
        
    }
}
