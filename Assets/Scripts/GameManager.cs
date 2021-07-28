using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum Scene { StartMenu, Gameplay }

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void NewGame_btn()
    {
        Scene scene = Scene.Gameplay;
        SceneManager.LoadScene(scene.ToString());
    }
    public void Exit_btn()
    {
        Application.Quit();
    }
    public void MainMenu_btn()
    {
        Scene scene = Scene.StartMenu;
        SceneManager.LoadScene(scene.ToString());
    }

}
