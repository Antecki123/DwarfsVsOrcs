using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum Scene { StartMenu, Gameplay }

    public GameObject settingsPanel;

    public Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start() => SetResolutionDropdown();

    private void SetResolutionDropdown()
    {
        if (resolutionDropdown)
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();

            int currentResolution = 0;
            
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                    currentResolution = i;
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolution;
            resolutionDropdown.RefreshShownValue();
        }
    }

    public void NewGame_btn()
    {
        Scene scene = Scene.Gameplay;
        SceneManager.LoadScene(scene.ToString());
    }

    public void MainMenu_btn()
    {
        Time.timeScale = 1f;
        Scene scene = Scene.StartMenu;
        SceneManager.LoadScene(scene.ToString());
    }

    public void Settings_Btn() => settingsPanel.SetActive(true);

    public void CloseSettings_Btn() => settingsPanel.SetActive(false);

    public void Exit_btn() => Application.Quit();

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen) => Screen.fullScreen = isFullscreen;
}
