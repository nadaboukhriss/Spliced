using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public Camera mainCamera;
    public Transform respawnPoint;
    public FadeScreen fadeScreen;
    public void Awake()
    {
        Instance = this;
    }


    public void QuitGame()
    {
        Application.Quit();

    }
    
}
