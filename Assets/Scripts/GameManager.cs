using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives = 3; 

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
