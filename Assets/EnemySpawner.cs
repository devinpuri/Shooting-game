using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;          // Enemy prefab to spawn.
    public int numberOfEnemiesPerSpawn = 3;   // Number of enemies to spawn at once.
    public float spawnCooldown = 5f;          // Time in seconds between each spawn cycle.
    public float spawnRadius = 1f;            // Optional: Radius around the spawner to randomize spawn positions.

    void Start()
    {
        // Start the spawn cycle.
        StartCoroutine(SpawnEnemiesRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        while (true)
        {
            // Spawn the designated number of enemies.
            for (int i = 0; i < numberOfEnemiesPerSpawn; i++)
            {
                // Instantiate the enemy prefab at the spawn position.
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            }
            // Wait for the cooldown time before the next spawn cycle.
            yield return new WaitForSeconds(spawnCooldown);
        }
    }
}
