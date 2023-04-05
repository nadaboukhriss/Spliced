using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDungeon : MonoBehaviour
{
    //Index found in main menu: File > Build Settings > Scenes in Build
    public int switchToSceneWithBuildIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //LoadSceneMode.Single vs .Additive - single vs mulitple levels loaded at once
            SceneManager.LoadScene(switchToSceneWithBuildIndex, LoadSceneMode.Single);
        }
    }
}
