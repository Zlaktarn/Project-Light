using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject helpMenu;

    public Dropdown resolutionDropdown;
    public Dropdown graphicsDropdown;

    public AudioMixer audioMixer;

    public bool settingsShow;
    public bool helpShow;

    Resolution[] resolutions;
    string[] qualities;

    private void Start()
    {
        //GRAPHICS ---->

        qualities = QualitySettings.names;

        graphicsDropdown.ClearOptions();
        
        List<string> graphicsList = new List<string>();

        int currentGraphicsIndex = 0;

        for (int i = 0; i < qualities.Length; i++)
        {
            graphicsList.Add(qualities[i]);

            if(QualitySettings.GetQualityLevel() == i)
            {
                currentGraphicsIndex = i;
                Debug.Log("lend me some sugar");
            }
        }

        graphicsDropdown.AddOptions(graphicsList);
        graphicsDropdown.value = currentGraphicsIndex;
        graphicsDropdown.RefreshShownValue();

        //RESOLUTION --->

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && !settingsShow && !helpShow) //check if keyDown 'p' while playing or being in settings or help menu
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
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        GameIsPaused = true;
    }

    public void LoadMenu() //Insert logic for scene change to main menu
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

    public void QuitGame() //insert logic for exiting the game
    {
        Debug.Log("Quitting Game...");
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
