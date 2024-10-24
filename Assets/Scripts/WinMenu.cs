using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class WinMenu : MonoBehaviour
{
    public GameObject winMenuUI;
    public GameManager gameManager;


    private void Start()
    {
        winMenuUI.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
    }

    public void ShowWinMenu()
    {
        winMenuUI.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void NextLevel()
    {
        winMenuUI.SetActive(false);
        gameManager.LoadNextLevel();
    }
}
