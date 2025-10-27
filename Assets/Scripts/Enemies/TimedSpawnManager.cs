using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class TimedSpawnEntry
    {
        public string name;
        public GameObject prefabToSpawn;
        public List<Transform> spawnPoints;
        public float spawnIntervalMin = 5f;
        public float spawnIntervalMax = 10f;
    }

    [Header("Timed Spawns")]
    public List<TimedSpawnEntry> spawnEntries = new List<TimedSpawnEntry>();

    void Start()
    {
        foreach (var entry in spawnEntries)
        {
            StartCoroutine(SpawnLoop(entry));
        }
    }

    IEnumerator SpawnLoop(TimedSpawnEntry entry)
    {
        while (true)
        {
            float waitTime = Random.Range(entry.spawnIntervalMin, entry.spawnIntervalMax);
            yield return new WaitForSeconds(waitTime);

            if (entry.spawnPoints.Count == 0 || entry.prefabToSpawn == null)
                continue;

            Transform randomPoint = entry.spawnPoints[Random.Range(0, entry.spawnPoints.Count)];
            Instantiate(entry.prefabToSpawn, randomPoint.position, randomPoint.rotation);
        }
    }
}
