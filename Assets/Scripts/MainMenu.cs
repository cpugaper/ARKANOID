using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Button continueButton;

    void Start()
    {
        if (!PlayerPrefs.HasKey("SavedLevel") || PlayerPrefs.GetInt("HasLost", 0) == 1)
        {
            continueButton.interactable = false;  
        }
        else
        {
            continueButton.interactable = true; 
        }
    }

    public void StartGame()
   {
        PlayerPrefs.DeleteAll(); 
        SceneManager.LoadScene("Level1");

        Time.timeScale = 1f;
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("SavedLevel") && PlayerPrefs.GetInt("HasLost", 0) == 0)
        {
            int savedLvl = PlayerPrefs.GetInt("SavedLevel");
            SceneManager.LoadScene(savedLvl);

            Time.timeScale = 1f;
        }
    }
}
