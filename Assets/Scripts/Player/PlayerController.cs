using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float thrustForce = 10f;
    public float torqueForce = 5f;
    public float rotationSmoothing = 5f;
    public float maxHeight = 20f;

    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public float projectileOffset = 1f;
    public Transform projectileSpawnPoint;

    private Rigidbody rb;
    private Vector3 targetTorque = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 2f;
        rb.angularDamping = 4f;
        rb.useGravity = false;
    }

    void Update()
    {
        HandleInput();
        ClampHeight();

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.ReturnToMainMenu();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void HandleInput()
    {
        float yaw = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float pitch = 0f;
        float roll = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) pitch = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) pitch = -1f;
        if (Input.GetKey(KeyCode.Q)) roll = 1f;
        if (Input.GetKey(KeyCode.E)) roll = -1f;

        targetTorque = new Vector3(pitch, yaw, roll) * torqueForce;
    }

    void ApplyMovement()
    {
        // Apply smooth torque
        Vector3 smoothedTorque = Vector3.Lerp(Vector3.zero, targetTorque, Time.fixedDeltaTime * rotationSmoothing);
        rb.AddRelativeTorque(smoothedTorque, ForceMode.Force);

        // Thrust forward/backward
        float thrustInput = 0f;
        if (Input.GetKey(KeyCode.W)) thrustInput = 1f;
        if (Input.GetKey(KeyCode.S)) thrustInput = -1f;

        Vector3 thrust = Vector3.forward * thrustInput * thrustForce;
        rb.AddRelativeForce(thrust, ForceMode.Force);
    }

    void ClampHeight()
    {
        if (transform.position.y > maxHeight)
        {
            Vector3 pos = transform.position;
            pos.y = maxHeight;
            transform.position = pos;
        }
    }

    void FireProjectile()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("Projectile prefab not assigned.");
            return;
        }

        Vector3 spawnPos = projectileSpawnPoint != null
            ? projectileSpawnPoint.position
            : transform.position + transform.forward * projectileOffset;

        Quaternion spawnRot = transform.rotation;
        Instantiate(projectilePrefab, spawnPos, spawnRot);
    }
}
