using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{

    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private Image iconImage;
    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float timeBetweenCharacters;

    private Queue<string> sentences;

    public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instances!");

        instance = this;
    }
    void Start()
    {
        dialoguePanel.SetActive(false);
        sentences = new Queue<string>();
    }
    public void StartDialogue(string[] dialogue,string name, Sprite icon)
    {
        Time.timeScale = 0;
        sentences.Clear();
        dialoguePanel.SetActive(true);
        //animator.SetBool("isOpen", true);
        playerInput.SwitchCurrentActionMap("Dialogue");
        nameText.text = name;
        iconImage.sprite = icon;
        foreach (string sentence in dialogue)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void OnDisplayNextSentence(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            DisplayNextSentence();
        }
    }
    public void DisplayNextSentence()
    {
        print("Next");
        if(sentences.Count == 0)
        {
            EndDialogue();
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
            yield return new WaitForSecondsRealtime(timeBetweenCharacters);
        }
    }

    void EndDialogue()
    {
        playerInput.SwitchCurrentActionMap("Player");
        //animator.SetBool("isOpen", false);
        dialoguePanel.SetActive(false);
        Time.timeScale = 1;
    }

}
