using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuUI;
    [SerializeField]
    private PlayerInput playerInput;


    public static bool isPaused = false;

    public static PauseManager instance;



    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Multiple instances!");

        instance = this;
    }

    public void OnNextTab(InputAction.CallbackContext context)
    {
        print("NextTab");
        if (context.started)
        {
            menuUI.GetComponent<TabGroup>().NextTab();
        }
    }
    public void OnPrevTab(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            menuUI.GetComponent<TabGroup>().PrevTab();
        }
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
