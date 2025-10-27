using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI healthText;

    private Health playerHealth;

    void Start()
    {
        FindPlayerHealth();
    }

    void Update()
    {
        // Update Score and Lives from GameManager
        if (GameManager.Instance != null)
        {
            scoreText.text = "Score: " + GameManager.Instance.score;
            livesText.text = "Lives: " + GameManager.Instance.lives;
        }

        // Update Health from player
        if (playerHealth != null)
        {
            healthText.text = "Health: " + playerHealth.currentHealth;
        }
        else
        {
            FindPlayerHealth(); // If the player respawns or scene reloads
        }
    }

    private void FindPlayerHealth()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }
    }
}
