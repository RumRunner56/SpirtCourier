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

    [Header("Victory Settings")]
    public int victoryScore = 1000; // Score required to win
    public string victorySceneName = "VictoryScene";

    [Header("Scene Names")]
    public string mainMenuSceneName = "MainMenu";
    public string gameOverSceneName = "GameOver";
    public string gameplaySceneName = "GameScene";

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

        // Check for new high score
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
        }

        // Check victory condition
        if (score >= victoryScore)
        {
            HandleVictory();
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

    public void LoseLife(GameObject player)
    {
        lives--;
        Debug.Log("Lost a life. Lives remaining: " + lives);

        if (lives > 0)
        {
            RespawnPlayer(player);
        }
        else
        {
            HandleGameOver();
        }
    }

    public void RespawnPlayer(GameObject player)
    {
        if (player == null) return;

        player.transform.position = Vector3.zero;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
#if UNITY_6_2_OR_NEWER
            rb.linearVelocity = Vector3.zero; // New Unity 6.2 physics API
#else
            rb.linearVelocity = Vector3.zero; // For older versions of Unity
#endif
            rb.angularVelocity = Vector3.zero;
        }

        Debug.Log("Player respawned at origin.");
    }

    public void ResetGame()
    {
        lives = startingLives;
        score = 0;
    }

    // --------------------------
    // 🔹 Scene Management
    // --------------------------

    public void HandleGameOver()
    {
        SaveHighScore();
        Debug.Log("Game Over - loading Game Over Scene");
        SceneManager.LoadScene(gameOverSceneName);
    }

    public void HandleVictory()
    {
        SaveHighScore();
        Debug.Log("Victory! Loading Victory Scene.");
        SceneManager.LoadScene(victorySceneName);
    }

    public void ReturnToMainMenu()
    {
        ResetGame();
        Debug.Log("Returning to Main Menu.");
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void StartNewGame()
    {
        ResetGame();
        Debug.Log("Starting new game.");
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quit.");
    }
}
