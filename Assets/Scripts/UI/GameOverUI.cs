using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        highScoreText.text = "High Score: " + GameManager.Instance.highScore;
    }

    public void ReturnToMainMenu()
    {
        GameManager.Instance.ReturnToMainMenu();
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}
