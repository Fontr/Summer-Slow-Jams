using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public bool dialogueIsActive = false, textIsTyping = false;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    private float speedTyping = 0.05f;
    public Button button;
    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
        if (dialogueIsActive) {
            if (Input.GetKeyDown(KeyCode.Space)) { DisplayNextSentence(); }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueIsActive = true;
        animator.SetBool("isOpen", true);
        button.gameObject.SetActive(false);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if (textIsTyping)
        {
            speedTyping = 0f;
            return;
        }

        else if (sentences.Count == 0)
        {
            EndDialogue(); 
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        textIsTyping = true;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(speedTyping); ;
        }
        textIsTyping = false;
        speedTyping = 0.05f;
    }

    private void EndDialogue()
    {
        dialogueIsActive = false;
        animator.SetBool("isOpen", false);
    }
}
