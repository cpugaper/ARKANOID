using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public GameObject winMenu;  

    public void ShowWinScreen()
    {
        winMenu.SetActive(true); 
        Time.timeScale = 0f; 
    }

    public void NextLevel()
    {
        PlayerPrefs.SetInt("Lives", FindObjectOfType<GameManager>().lives);
        PlayerPrefs.SetInt("Score", FindObjectOfType<GameManager>().score);
        PlayerPrefs.SetInt("MaxScore", maxSFindObjectOfType<GameManager>().maxScore);

        PlayerPrefs.SetInt("SavedLevel", SceneManager.GetActiveScene().buildIndex + 1);

        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LevelCompleted()
    {
        ShowWinScreen();
    }
}
