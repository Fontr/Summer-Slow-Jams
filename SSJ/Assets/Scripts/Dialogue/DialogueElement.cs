using TMPro;
using UnityEngine;

public class DialogueElement : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    private bool pressE = false;
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI popupPrompt;

    private void Start()
    {
        animator = GameObject.Find("playerEmotes").GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        //срабатывание диалога при нажатии клавиши в триггер-зоне
        if (pressE && Input.GetKey(KeyCode.E))
        {
            animator.SetBool("question", false);
            animator.SetBool("dialogue", true);
            TriggerDialogue();
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("question", true);
            popupPrompt.gameObject.SetActive(true);
            pressE = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.SetBool("question", false);
            popupPrompt.gameObject.SetActive(false);
            pressE = false;
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
