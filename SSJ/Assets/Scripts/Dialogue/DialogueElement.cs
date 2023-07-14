using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.UI;

public class DialogueElement : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    private bool pressE = false;

    private void FixedUpdate()
    {
        //срабатывание диалога при нажатии клавиши в триггер-зоне
        if (pressE && Input.GetKey(KeyCode.E))
        {
            TriggerDialogue();
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pressE = true;
            Debug.Log("Нажми E чтобы взаимодействовать");

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pressE = false;
            Debug.Log("Можешь не нажимать");
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
