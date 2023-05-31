using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public int mainMenuSceneIndex;
    
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneIndex, LoadSceneMode.Single);
    }
}
