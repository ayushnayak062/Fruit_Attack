using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{
    GameManager gameManager;
    public Text scoreText;
    public Text highScoreText;
    public Image[] stars;
    public Image starHolder;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.highScore= PlayerPrefs.GetFloat("Highscore");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.totalScore.ToString() != null)
        {
            scoreText.text = gameManager.totalScore.ToString();
            highScoreText.text = gameManager.highScore.ToString();
            if (gameManager.totalScore > gameManager.highScore)
            {
                PlayerPrefs.SetFloat("Highscore", gameManager.totalScore);
                
            }
            SetStars();
        }
        
    }
    void SetStars()
    {
        if (gameManager.mode == 1)
        {

            if (gameManager.timeRemaining < 5)

            {
                starHolder.sprite = stars[0].sprite;
            }
            else if (gameManager.timeRemaining > 5 && gameManager.timeRemaining < 10)
            {
                starHolder.sprite = stars[1].sprite;
            }
            else if (gameManager.timeRemaining > 10 && gameManager.timeRemaining < 15)
            {
                starHolder.sprite = stars[2].sprite;
            }
            else
            {
                starHolder.sprite = stars[2].sprite;
            }
        }

    }
}


