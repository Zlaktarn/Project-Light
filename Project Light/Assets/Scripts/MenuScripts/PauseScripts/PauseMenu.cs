using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    public GameObject pauseMenuUI;

    float oldSensitivity;
    public CameraMovement player;


    void Start()
    {
        GameIsPaused = false;
        oldSensitivity = player.sensitivity;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
                Resume();
            else 
                Pause();
        }

        if (GameIsPaused)
            player.sensitivity = 0;
        else
            player.sensitivity = oldSensitivity;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        GameIsPaused = true;
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
