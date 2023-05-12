using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Text scoreText; // Reference to the UI text element for displaying the score
    public Text highScoreText; // Reference to the UI text element for displaying the high score
    public Text timerText; // Reference to the UI text element for displaying the game timer
    public Text livesText; // Reference to the UI text element for displaying the remaining lives
    public int mode;

    public float gameDuration = 60.0f; // Duration of the game in seconds
    public string gameOverSceneName = "GameOver"; // Name of the scene to load on game over
    public string gameWinSceneName = "GameWin";// Name of the scene to load on game win
    public string gameWinEndlessName = "WinGameEndless";// Name of the scene to load on game win(endless)

    private float score = 0f; // Current score
    public  float highScore = 0f; // Current high score
    public float timeRemaining; // Time remaining in the game
    public int lives = 3; // Remaining lives
    public bool isShieldActive = false; // Flag to track if shield power-up is currently active
    public float totalScore;
    public float scoreMultiplierDuration = 10f; // Duration of the score multiplier power-up in seconds
    private bool isScoreMultiplierActive = false; // Flag to track if score multiplier power-up is currently active
    public float scoreMultiplierValue = 2f; // Multilier Value
    public int nextSceneLoad;
    public Animator transition;
    public AudioSource gamemanagerAudio;
    public AudioClip gameOverAudio;
    public AudioClip gamewinAudio;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        timeRemaining = gameDuration;
        UpdateScoreText();
        UpdateHighScoreText();
        UpdateLivesText();
        if (mode == 1)
        {
          UpdateTimerText();
          StartCoroutine(StartGameTimer());
        }
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }
   
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        
        
        // Update the high score if necessary
        if (totalScore > highScore)
        {
            highScore = totalScore;
            UpdateHighScoreText();
        }   
        if (isScoreMultiplierActive)
        {
            score *= scoreMultiplierValue; // Double the score value when score multiplier is active
        }

        totalScore += score; // Add the score to the total score
        UpdateScoreText();
    }

    public void WinGame()
    {
        if (SceneManager.GetActiveScene().buildIndex == 9||SceneManager.GetActiveScene().buildIndex == 22)
        {
            Debug.Log("You Completed ALL Levels");

        }
        else if(mode == 1)
        {
            PlayerPrefs.SetInt("levelReachedFestival",nextSceneLoad);
            LoadWinGameScene();
        }
        else if(mode == 2)
        {
            PlayerPrefs.SetInt("levelReachedCreampie", nextSceneLoad);
        }

    }

    public void DecreaseLife(int livesToDecrease)
    {
        lives -= livesToDecrease;
        UpdateLivesText();
      
        if (lives <= 0)
        {
            if (mode != 3)
                LoadGameOverScene();
            else
                LoadWinGameSceneEndless();
            
            
        }
    }
    public void Die()
    {
        if(mode != 3)
        {
            LoadGameOverScene();
        }
        else
        {
            LoadWinGameSceneEndless();
        }
    }
    public void AddLives(int livesToAdd)
    {
        // Increase player's lives by the given amount
        lives += livesToAdd;
        UpdateLivesText();
    }
    public void ActivateShield(float duration)
    {
        if (!isShieldActive)
        {
            isShieldActive = true; // Set shield flag to true
                                   // Add code to activate shield power-up, e.g., make player invulnerable to collisions for the specified duration
            StartCoroutine(DeactivateShieldAfterDuration(duration)); // Start a coroutine to deactivate the shield after the specified duration
        }
    }
    public void DeactivateShield()
    {
        isShieldActive = false; // Set shield flag to false
                                // Add code to deactivate shield power-up, e.g., reset player collision behavior
    }
    private IEnumerator DeactivateShieldAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration); // Wait for the specified duration
        DeactivateShield(); // Call method to deactivate shield
    }

    public void ActivateScoreMultiplier(float duration)
    {
        if (!isScoreMultiplierActive)
        {
            isScoreMultiplierActive = true; // Set score multiplier flag to true
            scoreMultiplierDuration = duration; // Set score multiplier duration
                                               
        }
    }
    private IEnumerator DeactivateScoreMultiplier()
    {
        yield return new WaitForSeconds(scoreMultiplierDuration);
        isScoreMultiplierActive = false; // Set score multiplier flag to false after duration
    }
    public bool IsScoreMultiplierActive()
    {
        return isScoreMultiplierActive;
    }
    private void UpdateScoreText()
    {
        scoreText.text =  totalScore.ToString();
    }

    private void UpdateHighScoreText()
    {
        highScoreText.text = highScore.ToString();
    }

    private void UpdateTimerText()
    {
        timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
    }

    private void UpdateLivesText()
    {
        livesText.text = "" + lives.ToString();
    }

    private IEnumerator StartGameTimer()
    {
        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timeRemaining -= 1.0f;
            UpdateTimerText();
        }

        // Game over logic here
        // You can add your game over logic in this function, such as displaying game over screen, resetting game state, etc.
        // Then, load the game over scene
        LoadGameOverScene();
    }

    private void LoadGameOverScene()
    {
        StartCoroutine(LoadLevel(gameOverSceneName));
    }
    private void LoadWinGameScene()
    {
        StartCoroutine(LoadLevel(gameWinSceneName));

    }
    private void LoadWinGameSceneEndless()
    {
        StartCoroutine(LoadLevel(gameWinEndlessName));
    }
    IEnumerator LoadLevel(string levelindex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelindex);
    }
}

