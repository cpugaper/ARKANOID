using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class WinMenu : MonoBehaviour
{
    public GameObject winMenuUI;
    private int brickCount;


    private void Start()
    {
        winMenuUI.SetActive(false);
        brickCount = GameObject.FindGameObjectsWithTag("Brick").Length;
    }

    public void OnBrickDestroyed()
    {
        brickCount--; 

        if (brickCount <= 0)
        {
            ShowWinMenu();
        }
    }

    void ShowWinMenu()
    {
        winMenuUI.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void NextLevel()
    {
        winMenuUI.SetActive(false);
        Time.timeScale = 1f;

        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int totalLevels = SceneManager.sceneCountInBuildSettings;

        if (nextLevelIndex >= totalLevels)
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            SceneManager.LoadScene(nextLevelIndex);
        }
    }
}
