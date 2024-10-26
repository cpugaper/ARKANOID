using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameManager gameManager;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; //Resumes game time
        gameManager.ButtonInteractability();
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //Stops game time
        gameManager.ButtonInteractability();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("SavedLevel", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
        Debug.Log("Game saved");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("MainMenu");
    }
}
