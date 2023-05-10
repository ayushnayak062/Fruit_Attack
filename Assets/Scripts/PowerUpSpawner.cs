using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; // Array of power-up prefabs to spawn
    public float spawnInterval = 5f; // Time interval between power-up spawns
    public float minX = -2f; // Minimum X-axis position for spawn
    public float maxX = 2f; // Maximum X-axis position for spawn

    private float nextSpawnTime; // Next time to spawn a power-up

    private void Start()
    {
        // Set initial spawn time
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        // Check if it's time to spawn a power-up
        if (Time.time >= nextSpawnTime)
        {
            // Generate random X-axis position within the specified range
            float randomX = Random.Range(minX, maxX);
            Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);

            // Spawn a random power-up from the array at the random X-axis position
            int randomIndex = Random.Range(0, powerUpPrefabs.Length);
            GameObject powerUpPrefab = powerUpPrefabs[randomIndex];
            Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = powerUpPrefab.GetComponent<Rigidbody2D>();
            rb.AddTorque(Random.Range(-10f, 10f));
            // Update next spawn time
            nextSpawnTime = Time.time + spawnInterval;
        }
    }
}