using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public Sprite icon;

    [TextArea(2,6)]
    public string[] sentences;
    
}
