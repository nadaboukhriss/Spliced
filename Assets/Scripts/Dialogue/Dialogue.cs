using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(2, 6)]
    public string[] sentences_to_human;
    public string[] sentences_to_fox;

    public string[] commonDialogue;

    

    public void TriggerItemDialogue(string name, Sprite icon)
    {
        DialogueManager.Instance.StartItemDialogue(sentences_to_human, name, icon);
    }

    public void TriggerDialogue(string name, Sprite icon, bool isHuman)
    {
        if (isHuman){
            DialogueManager.Instance.StartItemDialogue(sentences_to_human, name, icon);
        }
        else{
            DialogueManager.Instance.StartItemDialogue(sentences_to_fox, name, icon);
        }
        
    }

    public void TriggerDialogue(string name, Sprite icon)
    {
        DialogueManager.Instance.StartItemDialogue(commonDialogue, name, icon);
    }

}
