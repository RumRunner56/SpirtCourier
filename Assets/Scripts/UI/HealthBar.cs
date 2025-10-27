using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health targetHealth;
    public Image fillImage;
    public Vector3 offset = new Vector3(0, 2, 0);
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (targetHealth == null)
        {
            Destroy(gameObject); // Remove UI when target is gone
            return;
        }

        // Position the health bar above the target
        transform.position = targetHealth.transform.position + offset;

        // Always face the camera
        transform.LookAt(transform.position + mainCamera.transform.forward);

        // Update fill amount
        fillImage.fillAmount = (float)targetHealth.currentHealth / targetHealth.maxHealth;
    }
}
