using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float speed = 20f;
    public float lifetime = 5f;
    public int damage = 25;

    void Start()
    {
        Destroy(gameObject, lifetime); // Auto-destroy after time
    }

    void Update()
    {
        // Move forward every frame (framerate-independent)
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // Ignore self, other projectiles, or pickups
        if (other.CompareTag("Player") || other.CompareTag("Projectile") || other.isTrigger)
            return;

        // Try to apply damage to anything with a Health script
        Health targetHealth = other.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }

        // Destroy this projectile on impact
        Destroy(gameObject);
    }
}
