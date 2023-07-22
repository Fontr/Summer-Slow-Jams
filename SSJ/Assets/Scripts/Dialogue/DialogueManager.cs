using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    private bool dialogueIsActive = false, textIsTyping = false;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement player;
    private Animator playerAnimator;
    //скорость печати текста
    private float speedTyping = 0.05f;
    void Start()
    {
        sentences = new Queue<string>();
        playerAnimator = GameObject.Find("playerEmotes").GetComponent<Animator>();
    }

    void Update()
    {
        //отслеживание активен ли диалог
        if (dialogueIsActive) {
            if (Input.GetKeyDown(KeyCode.Space)) { DisplayNextSentence(); }
        }
    }

    //запуск диалога
    public void StartDialogue(Dialogue dialogue)
    {
        dialogueIsActive = true;
        player.isDialog = true;
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    //перебор сообщений диалога
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

    //конец диалога
    private void EndDialogue()
    {
        playerAnimator.SetBool("dialogue", false);
        dialogueIsActive = false;
        player.isDialog = false;
        animator.SetBool("isOpen", false);
    }
}
