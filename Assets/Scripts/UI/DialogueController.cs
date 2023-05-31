using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private TMP_Text dialogueText;

    [SerializeField]
    private float timeBetweenCharacters;
    public bool playerIsClose;

    public string[] dialogue;
    private int index;


    IEnumerator Typing(){
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
    }

    public void DisableAndReset(){
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    public void NextLine(){
        if(index < dialogue.Length - 1){
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }else{
            DisableAndReset();
        }
    }
    void Update(){
        /*if(playerIsClose){
            if(dialoguePanel.activeInHierarchy){
                DisableAndReset();
            }else{
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }*/
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            dialoguePanel.SetActive(true);
            StartCoroutine(Typing());
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerIsClose = false;
            DisableAndReset();
        }
    }

    
}
