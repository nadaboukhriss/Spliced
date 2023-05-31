using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public Camera mainCamera;
    public Transform respawnPoint;
    public FadeScreen fadeScreen;
    public int MenuIndex;
    public void Awake()
    {
        Instance = this;
        Time.timeScale = 1.0f;
    }


    public void QuitGame()
    {
        Application.Quit();

    }

    public void GoToMenu()
    {
        Time.timeScale = 1.0f; // Unfreeze
        SceneManager.LoadScene(MenuIndex, LoadSceneMode.Single);

    }
    
}
