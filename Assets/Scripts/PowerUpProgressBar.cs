using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpProgressBar : MonoBehaviour
{
    public Image magnetTimerBar; // Reference to the magnet power-up timer bar image
    public Image shieldTimerBar; // Reference to the shield power-up timer bar image
    public Image boostTimerBar; // Reference to the boost power-up timer bar image
    public PowerUpCollector powerUpCollector; // Reference to the PowerUpCollector script
    public GameObject MagnetTimer;
    public GameObject ShieldTimer;
    public GameObject ScoreMultiplierTimer;
    private void Start()
    {
        powerUpCollector = FindObjectOfType<PowerUpCollector>(); // Get reference to the PowerUpCollector script
        MagnetTimer.SetActive(false);
        ShieldTimer.SetActive(false);
        ScoreMultiplierTimer.SetActive(false);
    }

    private void Update()
    {
        // Check if magnet power-up is active
        if (powerUpCollector.isMagnetActive)
        {
            // Update magnet timer
            powerUpCollector.magnetTimer -= Time.deltaTime;

            // Update magnet timer bar fill amount
            if (magnetTimerBar != null)
            {
                magnetTimerBar.fillAmount = powerUpCollector.magnetTimer / powerUpCollector.magnetDuration;
                MagnetTimer.SetActive(true);
            }

            // Check if magnet timer has expired
            if (powerUpCollector.magnetTimer <= 0f)
            {
                
                MagnetTimer.SetActive(false);
                // Set magnet power-up active flag to false
                powerUpCollector.isMagnetActive = false;
                
                magnetTimerBar.fillAmount = 1;

            }
            else
            {
                // Call the magnet function
                powerUpCollector.Magnet();
            }
        }

        // Update shield timer bar fill amount
        if (powerUpCollector.isShieldActive)
        {
            powerUpCollector.shieldtimer -= Time.deltaTime; // Decrease shield duration by time elapsed
            if(shieldTimerBar != null)
            {
                shieldTimerBar.fillAmount = powerUpCollector.shieldtimer / powerUpCollector.shieldDuration; // Update shield timer bar fill amount
                
                ShieldTimer.SetActive(true);

            }
            if (powerUpCollector.shieldtimer <= 0)
            {
                ShieldTimer.SetActive(false);
                powerUpCollector.isShieldActive = false; // Set shield flag to false when duration expires
                GameManager.Instance.DeactivateShield(); // Call method to deactivate shield in the GameManager
            }
        }



    }
}
