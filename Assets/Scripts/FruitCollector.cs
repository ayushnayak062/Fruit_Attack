using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

[System.Serializable]
public class FruitInfo
{
    public string fruitTag; // Tag of the fruit
    public int numFruitsToWin; // Number of fruits of this type to collect to win the game
}

public class FruitCollector : MonoBehaviour
{
    public List<FruitInfo> fruitsToWin; // List of fruit information

    public int scorePerFruit = 10; // Score points earned per collected fruit
    public int lifeDecreasePerUnwantedItem = 1; // Number of lives to decrease per collected unwanted item

    public float comboResetTime = 2f; // Time in seconds since last fruit collected to reset combo
    private float lastFruitCollectedTime; // Time of last fruit collected
    private int comboMultiplier = 1; // Current combo multiplier
    public static int totalFruits = 0;
    public int requiedFruits = 0;
    public Text comboText; // Reference to the combo Text object in the UI

    
    public Dictionary<string, int> fruitsCollected; // Dictionary to keep track of fruits collected
    public Text requiredFruitsText; // Reference to the Text object in the UI for displaying required fruits
    public Text collectedFruitText; // Reference to the Text object in the UI for displaying collected fruits
    public Text requiredFruitText1; // Reference to the Text object in the UI for displaying collected fruits
    public GameManager gameManager;
    public AudioSource audioSourcefruit; // Reference to the AudioSource object for playing audio
    public AudioClip audioClipfruit; // Audio clip for collecting a fruit
    /*public AudioSource audioSourcebomb; // Reference to the AudioSource object for playing audio*/
    public AudioClip audioClipbomb; // Audio clip for collecting a fruit
    public AudioClip audiClipUnwanted;
    

    private void Start()
    {
        gameManager = GameManager.Instance;
        totalFruits = 0;
        // Initialize the fruitsCollected dictionary
        fruitsCollected = new Dictionary<string, int>();
        foreach (var fruitInfo in fruitsToWin)
        {
            fruitsCollected[fruitInfo.fruitTag] = 0; // Initialize count for each fruit to 0
        }
        
            // Update the requiredFruitsText in the UI
            UpdateRequiredFruitsText();
        
        

    }

    private void UpdateRequiredFruitsText()
    {
        if (GameManager.Instance.mode == 2)
        {
            string requiredFruits = "";
            string requireFruits1 = "";

            foreach (var fruitInfo in fruitsToWin)
            {
                requiredFruits += fruitInfo.fruitTag + " :\n";
                requireFruits1 += "/" + fruitInfo.numFruitsToWin.ToString() + "\n";

            }
            
            requiredFruitsText.text = requiredFruits;
            requiredFruitText1.text = requireFruits1;

            string collectedFruits = "";

            foreach (var fruit in fruitsCollected)
            {
                collectedFruits += fruit.Value.ToString() + "\n";
            }

            collectedFruitText.text = collectedFruits;
        }else if (GameManager.Instance.mode == 1)
        {
             
            requiredFruitsText.text = requiedFruits.ToString();
            collectedFruitText.text = totalFruits.ToString();

        }

 
    }






    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (collision.gameObject.tag)
        {
            case "Banana":
            case "Apple":
            case "Watermelon":
            case "Cherry":
            case "Mango":
            case "Strawberry":
            case "Pineapple":
            case "Grapes":
            case "Orange":
            case "Blackberries":
            case "Blueberries":
            case "Dragonfruit":
            case "Plump":
            case "Pear":
                // Check if collided object is a fruit
                {
                    // Get the tag of the collided fruit
                    string fruitTag = collision.gameObject.tag;
                    totalFruits++;
                    // Play the audio clip for collecting a fruit
                    audioSourcefruit.PlayOneShot(audioClipfruit);

                    // Check if enough time has passed since last fruit collected to reset combo
                    float timeSinceLastFruit = Time.time - lastFruitCollectedTime;
                    if (timeSinceLastFruit > comboResetTime)
                    {
                        comboMultiplier = 1; // Reset combo multiplier if time exceeds comboResetTime
                    }

                    // Add score for collecting a fruit multiplied by the combo multiplier
                    int score = scorePerFruit * comboMultiplier;
                    gameManager.AddScore(score);

                    // Update lastFruitCollectedTime and comboMultiplier
                    lastFruitCollectedTime = Time.time;
                    comboMultiplier++;

                    // Update the combo Text object in the UI
                    comboText.text = "Combo: x" + comboMultiplier.ToString();

                    // Destroy the collected fruit
                    Destroy(collision.gameObject);

                    // Update fruitsCollected dictionary
                    if (fruitsCollected.ContainsKey(fruitTag))
                    {
                        fruitsCollected[fruitTag]++; // Increment the count of the collected fruit
                    }
                    

                    // Check if the player has collected enough fruits of each type to win the game
                    bool allFruitsCollected = true;
                    foreach (var fruitInfo in fruitsToWin)
                    {

                        if (fruitsCollected[fruitInfo.fruitTag] < fruitInfo.numFruitsToWin)
                        {
                            allFruitsCollected = false; // Set to false if any fruit count is less than the required count
                            break;
                        }
                    }


                    // Update the requiredFruitsText in the UI
                    UpdateRequiredFruitsText();

                    if (allFruitsCollected && gameManager.mode == 2)
                    {
                        gameManager.WinGame(); // Call the WinGame() method in the GameManager
                    }
                }
                break;
            case "UnwantedItem":
                // Check if collided object is an unwanted item
                {
                    audioSourcefruit.PlayOneShot(audiClipUnwanted);

                    if (!gameManager.isShieldActive)
                    {
                        // Decrease life for collecting an unwanted item
                        gameManager.DecreaseLife(lifeDecreasePerUnwantedItem);

                        // Destroy the collected unwanted item
                        Destroy(collision.gameObject);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                    }

                }
                break;
            case "Bomb":
                audioSourcefruit.PlayOneShot(audioClipbomb);
                if (!gameManager.isShieldActive)
                {
                    // Decrease life for collecting an unwanted item
                    gameManager.Die();

                    // Destroy the collected unwanted item
                    Destroy(collision.gameObject);
                }
                else
                {
                    Destroy(collision.gameObject);
                }

                break;

            default:
                Debug.Log("q");
                break;
        }
    }
       
    
    private void Update()
    {
        if (totalFruits == requiedFruits &&gameManager.mode == 1)
        {
           gameManager.WinGame();
        }
    }

}






