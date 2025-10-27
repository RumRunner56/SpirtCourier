using UnityEngine;

public class PointPickup : MonoBehaviour
{
    public int pointValue = 10;
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add score
            GameManager.Instance.AddScore(pointValue);

            // Optional: play sound
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            Destroy(gameObject);
        }
    }
}
