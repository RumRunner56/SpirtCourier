using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Health Bar")]
    public GameObject healthBarPrefab;
    private GameObject healthBarInstance;

    [Header("Audio")]
    public AudioClip healthPickupSound;
    public float healthPickupVolume = 1f;

    private bool isPlayer;

    void Awake()
    {
        currentHealth = maxHealth;
        isPlayer = CompareTag("Player");
    }

    void Start()
    {
        // Spawn floating health bar if prefab is assigned
        if (healthBarPrefab != null)
        {
            healthBarInstance = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
            HealthBar bar = healthBarInstance.GetComponent<HealthBar>();
            if (bar != null)
            {
                bar.targetHealth = this;
            }
        }
    }

    // ----------------------------------------
    // 🔹 Public: Add Health
    // ----------------------------------------

    public void AddHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log(gameObject.name + " healed to " + currentHealth);

        if (healthPickupSound != null)
        {
            AudioSource.PlayClipAtPoint(healthPickupSound, transform.position, healthPickupVolume);
        }
    }

    // ----------------------------------------
    // 🔹 Public: Take Damage
    // ----------------------------------------

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);
        Debug.Log(gameObject.name + " took " + amount + " damage. Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    // ----------------------------------------
    // 🔹 Handle Death
    // ----------------------------------------

    private void HandleDeath()
    {
        if (healthBarInstance != null)
            Destroy(healthBarInstance);

        if (isPlayer)
        {
            GameManager.Instance.LoseLife(gameObject);

            if (GameManager.Instance.lives > 0)
            {
                // Respawn player at origin
                currentHealth = maxHealth;
                transform.position = Vector3.zero;

                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }

                Debug.Log("Player respawned. Lives left: " + GameManager.Instance.lives);
            }
            else
            {
                GameManager.Instance.HandleGameOver();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ----------------------------------------
    // 🔹 Cleanup
    // ----------------------------------------

    private void OnDestroy()
    {
        if (healthBarInstance != null)
        {
            Destroy(healthBarInstance);
        }
    }
}
