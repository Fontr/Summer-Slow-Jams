using System.Collections;

using TMPro;

using UnityEngine;


public class TextPrestart : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] sentences;
    private bool textIsTyping = false;
    private int numText = 0;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject endScene;

    public void Start()
    {
        DisplayNextSentence();
    }

    void Update()
    {
        //проверка событий нажатием кнопок
        if (Input.GetKeyDown(KeyCode.Mouse0) && !textIsTyping) { DisplayNextSentence(); }
    }

    public void DisplayNextSentence()
    {
        if (textIsTyping)
        {
            return;
        }

        else if (numText == 4)
        {
            EndDialogue();
            return;
        }

        StartCoroutine(TypeSentence(sentences[numText]));
        numText++;
    }

    IEnumerator TypeSentence(string sentence)
    {
        textIsTyping = true;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        textIsTyping = false;
    }

    //конец диалога
    public void EndDialogue()
    {
        endScene.SetActive(true);
    }
}

