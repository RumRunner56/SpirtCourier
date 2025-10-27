using UnityEngine;

public class PickupFloat : MonoBehaviour
{
    public float floatAmplitude = 0.25f;  // How far it moves up and down
    public float floatSpeed = 2f;         // How fast it moves
    public float rotationSpeed = 45f;     // Degrees per second

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Vertical bobbing motion
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = startPos + new Vector3(0, newY, 0);

        // Optional rotation
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
