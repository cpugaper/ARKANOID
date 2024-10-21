using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static BrickFactory;

public class GameManager : MonoBehaviour
{
    private int lives = 3;
    private int score = 0;
    public Text livesText;
    public Text scoreText;
    public Text maxScoreText;

    private int maxScore = 0; 

    void Start()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            int savedLvl = PlayerPrefs.GetInt("SavedLevel");

            if (SceneManager.GetActiveScene().buildIndex != savedLvl)
            {
                SceneManager.LoadScene(savedLvl);
            }
        }

        maxScore = PlayerPrefs.GetInt("MaxScore", 0);

        UpdateHUD();

        AsignBrickType(); 
    }

    public void AsignBrickType()
    {
        Brick[] bricks = GetComponentsInChildren<Brick>();

        foreach (Brick brick in bricks)
        {
            BrickType randomType = (BrickType)Random.Range(0, 3);
            BrickFactory.ConfigureBrick(brick, randomType);
        }
    }

    public void LoseHealth()
    {
        lives--;

        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOver"); 
        }
        else
        {
            ResetLevel(); 
        }

        UpdateHUD(); 
    }

    public void ResetLevel()
    {
        FindObjectOfType<Ball>().ResetBall();
        FindObjectOfType<Player>().ResetPlayer();
    }

    public void CheckLevelCompleted()
    {
        if (transform.childCount <= 1)
        {
            //buildIndex = buildSettings Index
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void AddScore(int points)
    {
        score += points; 

        if(score > maxScore)
        {
            maxScore = score;
            PlayerPrefs.SetInt("MaxScore", maxScore);
        }

        UpdateHUD();
    }

    private void UpdateHUD()
    {
        if (livesText != null && scoreText != null && maxScoreText != null)
        {
            livesText.text = "LIVES: " + lives;
            scoreText.text = "SCORE: " + score;
            maxScoreText.text = "RECORD: " + maxScore;
        }
    }
}
