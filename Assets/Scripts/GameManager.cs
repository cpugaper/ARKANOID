using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using static BrickFactory;

public class GameManager : MonoBehaviour
{
    private int lives = 3;
    private int score = 0; 
    private int maxScore = 0; 

    public Text livesText;
    public Text scoreText;
    public Text maxScoreText;

    void Start()
    {

        int hasLost = PlayerPrefs.GetInt("HasLost", 0);

        if (hasLost == 1)
        {
            PlayerPrefs.SetInt("SavedLevel", 0);
            PlayerPrefs.GetInt("Lives", 3);
            PlayerPrefs.GetInt("Score", 0);
            PlayerPrefs.GetInt("HasLost", 0);

            Debug.Log("Saved game cannot be loaded because you lost");
        }
        else if (PlayerPrefs.HasKey("SavedLevel"))
        {
            int savedLvl = PlayerPrefs.GetInt("SavedLevel");

            if (SceneManager.GetActiveScene().buildIndex != savedLvl)
            {
                SceneManager.LoadScene(savedLvl);
            }
        }

        lives = PlayerPrefs.GetInt("Lives", 3);
        score = PlayerPrefs.GetInt("Score", 0);
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
            PlayerPrefs.SetInt("HasLost", 1);
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
            int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            int totalLevels = SceneManager.sceneCountInBuildSettings - 2; // -2 to not take into account the GameOver and Main Menu scenes

            PlayerPrefs.SetInt("Lives", FindObjectOfType<GameManager>().lives);
            PlayerPrefs.SetInt("Score", FindObjectOfType<GameManager>().score);
            PlayerPrefs.SetInt("MaxScore", FindObjectOfType<GameManager>().maxScore);

            PlayerPrefs.SetInt("SavedLevel", SceneManager.GetActiveScene().buildIndex + 1);

            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            // if we complete last level, return to level1 
            if (nextLevel >= totalLevels)
            {
                PlayerPrefs.SetInt("SavedLevel", 1);
                SceneManager.LoadScene("Level1");
            }
            else
            {
                PlayerPrefs.SetInt("SavedLevel", nextLevel);
                SceneManager.LoadScene(nextLevel);
            }

            PlayerPrefs.Save(); 
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
