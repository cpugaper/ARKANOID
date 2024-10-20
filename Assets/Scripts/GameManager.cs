using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static BrickFactory;

public class GameManager : MonoBehaviour
{
    public int lives = 3;

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

        AsignBrickType(); 
    }

    public void AsignBrickType()
    {
        Brick[] bricks = GetComponentsInChildren<Brick>();
        BrickFactory factory = new BrickFactory();

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
}
