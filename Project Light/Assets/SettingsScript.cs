using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject helpMenu;

    public bool settingsShow;
    public bool helpShow;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && !settingsShow && !helpShow)
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

    public void HelpButton()
    {
        pauseMenuUI.SetActive(false);
        helpMenu.SetActive(true);
        helpShow = true;
    }

    public void BackFromHelpButton()
    {
        helpMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
        helpShow = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
    }
}
