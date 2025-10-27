using UnityEngine;

public class GhostTrapAI : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float stopDistance = 1.5f;
    public float rotationSpeed = 5f;

    [Header("Attack Settings")]
    public int damageAmount = 25;
    public float attackCooldown = 1.5f; // seconds between attacks

    private Transform player;
    private float nextAttackTime = 0f;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Rotate to face player
        Vector3 lookDir = player.position - transform.position;
        lookDir.y = 0; // Ignore vertical rotation
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(lookDir),
            rotationSpeed * Time.deltaTime
        );

        // Move toward player
        if (distance > stopDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;

            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("GhostTrap hit player for " + damageAmount + " damage!");
            }
        }
    }
}
