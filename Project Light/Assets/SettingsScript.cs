using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    public bool settingsShow;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && !settingsShow)
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu...");
    }

    public void ShowSettings()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        settingsShow = true;
    }

    public void BackFromSettingsButton()
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        settingsShow = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
    }
}
