using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadAudio : MonoBehaviour
{
    void Awake() 
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
