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
    
    public WinMenu winMenu;
    public PauseMenu pauseMenu;   
    
    public Button autoButton;
    public Button pauseButton;

    void Start()
    {
        winMenu = FindObjectOfType<WinMenu>();
        pauseMenu = FindObjectOfType< PauseMenu>();

        if (PlayerPrefs.GetInt("HasLost", 0) == 1)
        {
            ResetPlayerPrefs(); 
            Debug.Log("Saved game cannot be loaded because you lost");
        }
        else
        {
            LoadSavedLevel(); 
        }

        lives = PlayerPrefs.GetInt("Lives", 3);
        score = PlayerPrefs.GetInt("Score", 0);
        maxScore = PlayerPrefs.GetInt("MaxScore", 0);

        ButtonInteractability();
        UpdateHUD();
        AsignBrickType();
    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.SetInt("SavedLevel", 0);
        PlayerPrefs.GetInt("Lives", 3);
        PlayerPrefs.GetInt("Score", 0);
        PlayerPrefs.GetInt("HasLost", 0);
    }

    private void LoadSavedLevel()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            int savedLevel = PlayerPrefs.GetInt("SavedLevel", 0);
            if (SceneManager.GetActiveScene().buildIndex != savedLevel)
            {
                SceneManager.LoadScene(savedLevel);
            }
        }
    }

    public void ButtonInteractability()
    {
        bool isAnyMenuActive = (winMenu != null && winMenu.winMenuUI.activeSelf) || (pauseMenu != null && pauseMenu.pauseMenuUI.activeSelf);
        if (autoButton != null) autoButton.interactable = !isAnyMenuActive;
        if (pauseButton != null) pauseButton.interactable = !isAnyMenuActive;
    }

    public void AsignBrickType()
    {
        foreach (var brick in GetComponentsInChildren<Brick>())
        {
            BrickFactory.ConfigureBrick(brick, (BrickType)Random.Range(0, 3));
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
            winMenu.ShowWinMenu();
        }
    }

    public void LoadNextLevel()
    { 
        PlayerPrefs.SetInt("Lives", lives);
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("MaxScore", maxScore);
        PlayerPrefs.SetInt("SavedLevel", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene(GetNextLevelName(SceneManager.GetActiveScene().name));
        
        Time.timeScale = 1f;
    }

    private string GetNextLevelName(string currentLevel)
    {
        switch (currentLevel)
        {
            case "Level1":
                return "Level2";
            case "Level2":
                return "Level1";
            default:
                return "Level1";
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
