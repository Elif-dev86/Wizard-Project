using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{

    //[HideInInspector]
    public InputActionAsset actions;

    [HideInInspector]
    public InputAction pause;

    public GameObject pauseMenu;

    public bool canPause = true;

    public bool isPaused = false;

    void Start()
    {
        pause =actions.FindActionMap("Gameplay").FindAction("pause");
        pause.performed += PauseGame;

        pauseMenu = GameObject.FindGameObjectWithTag("pauseMenu");
        pauseMenu.SetActive(false);
    }

    void PauseGame(InputAction.CallbackContext ctx)
    {
        if (canPause)
        {
            if (ctx.performed)
            {
                isPaused = !isPaused;
            }

            if (isPaused)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }

    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnEnable()
    {
        actions.Enable();
    }

    public void OnDisable()
    {
        actions.Disable();
    }
}
