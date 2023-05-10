using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PowerUpCollector : MonoBehaviour
{
    public string[] powerUpTags; // Tags of the power-up objects
    public string[] fruitTags; // Tags of the fruit objects
    public int[] scoreValues; // Score values for collecting each power-up
    public float magnetRange = 5f; // Range of the magnet power-up
    public float magnetDuration = 10f; // Duration of the magnet power-up in seconds
    public float magnetPower = 1f; // Strength of the magnet power-up
    public float shieldDuration = 10f; // Duration of the shield power-up in seconds
    public  bool isShieldActive = false; // Flag to track if shield power-up is currently active
    private bool isScoreMultiplierActive = false; // Flag to track if score multiplier power-up is active


    private GameManager gameManager; // Reference to the game manager
    public bool isMagnetActive; // Flag to track if magnet power-up is active
    public float magnetTimer; // Timer for magnet power-up duration
    public float shieldtimer;
    public AudioSource powerUpAudio;
    public AudioClip magnetAudioClip;
    public AudioClip shieldAudioClip;
    public AudioClip healthAudioClip;
    public AudioClip magScoreMultiplierAudiClip;


    private void Start()
    {
        // Find the game manager object and get its GameManager component
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        // Check if magnet power-up is active
        if (isMagnetActive)
        {
            // Update magnet timer
            magnetTimer -= Time.deltaTime;

            // Check if magnet timer has expired
            if (magnetTimer <= 0f)
            {
                // Set magnet power-up active flag to false
                isMagnetActive = false;
            }
            else
            {
                // Call the magnet function
                Magnet();
            }
        }
        if (isShieldActive)
        {
            shieldtimer -= Time.deltaTime; // Decrease shield duration by time elapsed
            if (shieldtimer <= 0f)
            {
                isShieldActive = false; // Set shield flag to false when duration expires
                GameManager.Instance.DeactivateShield(); // Call method to deactivate shield in the GameManager
            }
        }
        // Check if score multiplier power-up is active
        if (isScoreMultiplierActive)
        {
            // Update score multiplier timer
            GameManager.Instance.scoreMultiplierDuration -= Time.deltaTime;

            // Check if score multiplier timer has expired
            if (GameManager.Instance.scoreMultiplierDuration <= 0f)
            {
                // Set score multiplier power-up active flag to false
                isScoreMultiplierActive = false;
            }
        }
    }

    // Function to apply magnet power-up effect
    public void Magnet()
    {
        // Find all the fruit objects within the magnet range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, magnetRange);

        // Loop through all the found colliders
        foreach (Collider2D collider in colliders)
        {
            // Check if the collider has any of the fruit tags
            for (int i = 0; i < fruitTags.Length; i++)
            {
                if (collider.CompareTag(fruitTags[i]))
                {
                    // Move the fruit object towards the collector with magnet power
                    Vector3 direction = transform.position - collider.transform.position;
                    collider.transform.position += direction.normalized * Time.deltaTime * magnetPower;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Loop through all the power-up tags and check if the collided object has any of them
        for (int i = 0; i < powerUpTags.Length; i++)
        {
            if (other.CompareTag(powerUpTags[i]))
            {
                // Check if the collected power-up is a magnet
                if (powerUpTags[i] == "Magnet")
                {
                    powerUpAudio.PlayOneShot(magnetAudioClip);
                    // Set magnet power-up active flag to true
                    isMagnetActive = true;
                    

                    // Set magnet timer to duration
                    magnetTimer = magnetDuration;

                    // Destroy the collected power-up object
                    Destroy(other.gameObject);
                }
                // Check if the collected power-up is a heart
                else if (powerUpTags[i]=="Heart")
                {
                    powerUpAudio.PlayOneShot(healthAudioClip);
                    // Increase player's lives by 1
                    gameManager.AddLives(1);

                    // Destroy the collected power-up object
                    Destroy(other.gameObject);
                }
                else if (powerUpTags[i] == "Shield" ) // Check if collided object is a shield power-up
                {
                    powerUpAudio.PlayOneShot(shieldAudioClip);
                    // Activate shield power-up
                    if (!isShieldActive)
                    {
                        shieldtimer = shieldDuration;
                        isShieldActive = true; // Set shield flag to true
                        GameManager.Instance.ActivateShield(shieldDuration); // Call method to activate shield in the GameManager with the specified duration
                        Destroy(other.gameObject); // Destroy the collected shield power-up
                    }
                }
                else if (powerUpTags[i] == "ScoreMultiplier")
                {
                    powerUpAudio.PlayOneShot(magScoreMultiplierAudiClip);
                    // Set score multiplier power-up active flag to true
                    isScoreMultiplierActive = true;

                    // Set score multiplier timer to duration
                    GameManager.Instance.scoreMultiplierDuration = GameManager.Instance.scoreMultiplierDuration;

                    // Destroy the collected power-up object
                    Destroy(other.gameObject);
                }
                else
                {
                    // Add score value to the game manager
                    gameManager.AddScore(scoreValues[i]);

                    // Destroy the collected power-up object
                    Destroy(other.gameObject);
                }

                break; // Exit the loop after finding a matching tag
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a Gizmos sphere to visualize the magnet range
        if (isMagnetActive)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, magnetRange);
        }
    }
}



