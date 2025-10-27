using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject objectToSpawn;

    [Tooltip("Should the object spawn on Start?")]
    public bool spawnOnStart = true;

    private GameObject spawnedInstance;

    void Start()
    {
        if (spawnOnStart && objectToSpawn != null)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        if (spawnedInstance == null)
        {
            spawnedInstance = Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
    }

    public void ClearSpawn()
    {
        if (spawnedInstance != null)
        {
            Destroy(spawnedInstance);
        }
    }

    public bool HasSpawned => spawnedInstance != null;
}
