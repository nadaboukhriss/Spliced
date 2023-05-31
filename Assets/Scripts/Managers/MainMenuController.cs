using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int gameSceneIndex;
    
    public void NewGame()
    {
        SceneManager.LoadScene(gameSceneIndex, LoadSceneMode.Single);
    }
    public void Continue()
    {
        SceneManager.LoadScene(gameSceneIndex, LoadSceneMode.Single);
    }
}
