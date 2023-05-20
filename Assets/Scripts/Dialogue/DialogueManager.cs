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
    private PlayerInput playerInput;

    [SerializeField]
    private float timeBetweenCharacters;

    private Queue<string> sentences;

    public static DialogueManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogWarning("Multiple instances!");

        Instance = this;

        
    }

    void Start()
    {
        dialoguePanel.SetActive(false);
        sentences = new Queue<string>();
        playerInput = GameManager.Instance.player.GetComponent<PlayerInput>();
    }
    public void StartDialogue(string[] dialogue,string name, Sprite icon)
    {
        Time.timeScale = 0;
        sentences.Clear();
        dialoguePanel.SetActive(true);
        playerInput.SwitchCurrentActionMap("Dialogue");
        nameText.text = name;
        if(icon != null)
        {
            iconImage.sprite = icon;
            Color color = iconImage.color;
            color.a = 1.0f;
            iconImage.color = color;
        }
        else
        {
            Color color = iconImage.color;
            color.a = 0.0f;
            iconImage.color = color;
        }
        
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
        dialoguePanel.SetActive(false);
        Time.timeScale = 1;
    }

}
