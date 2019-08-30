using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Dialogue[] dialogues;
    public List<string> sentences;

    public Animator anim;

    private void Update()
    {
        if (anim.GetBool("IsOpen"))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                DisplayNextSentence();
            }
        }   
    }

    public void StartDialogue(int index)
    {
        anim.SetBool("IsOpen", true);
        nameText.text = dialogues[index].name;

        foreach (string sentence in dialogues[index].sentences)
        {
            sentences.Insert(0,sentence);
        }

        DisplayNextSentence();
    } 

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences[sentences.Count-1];
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        sentences.RemoveAt(sentences.Count-1);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        anim.SetBool("IsOpen", false);
    }
}
