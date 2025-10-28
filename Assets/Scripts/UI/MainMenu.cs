using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1_HauntedForest");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
