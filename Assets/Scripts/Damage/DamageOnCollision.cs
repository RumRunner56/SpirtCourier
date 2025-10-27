using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public int damageAmount = 25;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
                Debug.Log("Player hit trap! Took " + damageAmount + " damage.");
            }
        }
    }
}
