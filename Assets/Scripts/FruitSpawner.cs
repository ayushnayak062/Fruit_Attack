using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruits; // Array of fruit game objects
    public GameObject[] unwantedItems; // Array of unwanted item game objects
    public float spawnInterval = 1f; // Initial interval at which fruits and unwanted items will spawn
    public float spawnSpeedMin = 1f; // Minimum speed at which fruits and unwanted items will fall
    public float spawnSpeedMax = 2f; // Maximum speed at which fruits and unwanted items will fall
    public float spawnPosXMin = -2f; // Minimum X position at which fruits and unwanted items will spawn
    public float spawnPosXMax = 2f; // Maximum X position at which fruits and unwanted items will spawn
    public float spawnRateIncreasePerSecond = 0.1f; // Rate at which spawn rate will increase per second
    public float spawnSpeedIncreasePerSecond = 0.1f; // Rate at which fall speed will increase per second

    private float spawnIntervalTimer; // Timer for tracking spawn interval
    private float timeSinceStart; // Time elapsed since the script started
    
    private void Start()
    {
        // Start spawning fruits and unwanted items
        spawnIntervalTimer = spawnInterval;
    }

    private void Update()
    {
        // Update time elapsed
        timeSinceStart += Time.deltaTime;

        // Increase spawn rate and fall speed with time
        spawnIntervalTimer -= Time.deltaTime;
        if (spawnIntervalTimer <= 0f)
        {
            spawnInterval -= spawnRateIncreasePerSecond * Time.deltaTime;
            spawnSpeedMin += spawnSpeedIncreasePerSecond * Time.deltaTime;
            spawnSpeedMax += spawnSpeedIncreasePerSecond * Time.deltaTime;

            spawnIntervalTimer = spawnInterval;
        }

        // Spawn objects at the updated spawn rate
        if (timeSinceStart >= spawnInterval)
        {
            SpawnObject();
            timeSinceStart = 0f;
        }
    }

    private void SpawnObject()
    {
        // Randomly choose a spawn position along the X axis
        float spawnPosX = Random.Range(spawnPosXMin, spawnPosXMax);

        // Randomly choose a fruit or unwanted item to spawn
        GameObject objToSpawn = GetRandomObject();

        // Instantiate the chosen object at the spawn position
        GameObject spawnedObj = Instantiate(objToSpawn, new Vector3(spawnPosX, transform.position.y, 0f), Quaternion.identity);

        // Randomly choose a speed for the spawned object to fall
        float spawnSpeed = Random.Range(spawnSpeedMin, spawnSpeedMax);

        // Set the fall speed and spin for the spawned object
        Rigidbody2D rb = spawnedObj.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, -spawnSpeed);
        
        if (rb.gameObject.CompareTag("UnwantedItem"))
        {
            rb.AddTorque(Random.Range(-5f, 5f)); // rb.AddTorque(5f); // Add a random spin between -5 and 5 degrees per fixed frame

        }
        else
            rb.AddTorque( Random.Range(-30f, 30f));
        
       
       
    }

    private GameObject GetRandomObject()
    {
        // Randomly choose between a fruit or unwanted item
        float randomValue = Random.value;
        if (randomValue < 0.7f) // 70% chance to spawn a fruit
        {
            int randomIndex = Random.Range(0, fruits.Length);
            return fruits[randomIndex];
        }
        else // 30% chance to spawn an unwanted item
        {
            int randomIndex = Random.Range(0, unwantedItems.Length);
            return unwantedItems[randomIndex];
        }
    }
}
