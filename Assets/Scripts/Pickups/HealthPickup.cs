using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 25;
    public int scoreBonus = 5;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.AddHealth(healAmount);
            }

            GameManager.Instance.AddScore(scoreBonus);
            Destroy(gameObject);
        }
    }
}
