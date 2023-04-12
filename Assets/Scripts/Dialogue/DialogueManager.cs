using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private Animator animator;

    [SerializeField]
    private float timeBetweenCharacters;

    private Queue<string> sentences;


    void Start()
    {
        sentences = new Queue<string>();   
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        Time.timeScale = 0;
        sentences.Clear();
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;
        iconImage.sprite = dialogue.icon;
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
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
        animator.SetBool("isOpen", false);
        Time.timeScale = 1;
    }

}
