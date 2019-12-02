using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject dialogueManager;
    private DialogueTrigger trigger;
    public GameObject deactivate;

    private bool enter = false;

    void Start()
    {
        trigger = gameObject.GetComponent<DialogueTrigger>();
    }

    private void OnTriggerEnter(Collider col)
    {
        enter = true;

        if(col.transform.gameObject.name == "Player")
        {
            if (enter)
            {
                trigger.TriggerDialogue();
                deactivate.SetActive(false);
            }
        }
    }
}
