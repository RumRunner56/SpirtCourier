using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Gameplay Settings")]
    public int startingLives = 3;
    public int lives;
    public int score;
    public int highScore;

    [Header("Scene Names")]
    public string mainMenuSceneName = "MainMenu";
    public string gameOverSceneName = "GameOver";

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ResetGame();
        LoadHighScore();
    }

    // --------------------------
    // 🔹 Score Management
    // --------------------------

    public void AddScore(int amount)
    {
        score += amount;

        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
        }
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // --------------------------
    // 🔹 Life & Respawn System
    // --------------------------

    public void LoseLife()
    {
        lives--;
        Debug.Log("Lost a life. Lives remaining: " + lives);
    }

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = Vector3.zero;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void ResetGame()
    {
        lives = startingLives;
        score = 0;
    }

    // --------------------------
    // 🔹 Game Over & Scene Flow
    // --------------------------

    public void HandleGameOver()
    {
        SaveHighScore();
        SceneManager.LoadScene(gameOverSceneName);
    }

    public void ReturnToMainMenu()
    {
        ResetGame();
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quit.");
    }
}
