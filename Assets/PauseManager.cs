using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuUI;
    private PlayerInput playerInput;


    public static bool isPaused = false;

    public static PauseManager Instance;



    private void Awake()
    {
        if (Instance != null)
            Debug.LogWarning("Multiple instances!");

        Instance = this;
    }

    private void Start()
    {
        playerInput = GameManager.Instance.player.GetComponent<PlayerInput>();
    }

    public void OnSwitchTab(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float value = context.ReadValue<float>();
            if (value < 0)
            {
                menuUI.GetComponent<TabGroup>().PrevTab();
            }
            else if (value > 0)
            {
                menuUI.GetComponent<TabGroup>().NextTab();
            }
        }
    }
    //InputAction.CallbackContext context
    public void TogglePause()
    {
        if (isPaused)
        {
            playerInput.SwitchCurrentActionMap("Player");
            Time.timeScale = 1;
            isPaused = false;
            menuUI.SetActive(false);
        }
        else
        {
            playerInput.SwitchCurrentActionMap("PauseMenu");
            Time.timeScale = 0;
            isPaused = true;
            menuUI.SetActive(true);
        }
        
    }



}
