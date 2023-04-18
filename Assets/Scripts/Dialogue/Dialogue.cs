using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(2, 6)]
    public string[] sentences;

    public void TriggerDialogue(string name, Sprite icon)
    {
        DialogueManager.instance.StartDialogue(sentences, name, icon);
    }

}
