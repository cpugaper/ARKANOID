using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int brickLife;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        brickLife--;

        if (brickLife <= 0)
        {
            FindObjectOfType<GameManager>().CheckLevelCompleted(); 
            Destroy(gameObject);
        }
    }
}
