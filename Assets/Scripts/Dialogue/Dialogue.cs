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
        DialogueManager.Instance.StartDialogue(sentences, name, icon);
    }

}
