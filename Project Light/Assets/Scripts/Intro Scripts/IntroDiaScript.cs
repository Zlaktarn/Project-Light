using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroDiaScript : MonoBehaviour
{
    Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;

    //public Animator animator;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(IntroDia dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    //void EndDialogue()
    //{
    //    animator.SetBool("IsOpen", false);
    //}
}