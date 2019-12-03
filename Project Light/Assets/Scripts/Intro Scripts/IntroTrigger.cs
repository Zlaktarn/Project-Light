using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTrigger : MonoBehaviour
{
    public IntroDia dialogue;
    
    public void TriggerDialogue()
    {
        //FindObjectOfType<IntroDiaScript>().StartDialogue(dialogue);
    }

    private void Start()
    {
        FindObjectOfType<IntroDiaScript>().StartDialogue(dialogue);
    }
}
