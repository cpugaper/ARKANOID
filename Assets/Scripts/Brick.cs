using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int brickLife;
    public int points = 10;

    public GameObject powerUpPrefab;
    public float dropRate = 0.2f;

    private SpriteRenderer spriteRenderer;
    private float opacityDecrease = 0.5f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        brickLife--;

        Color color = spriteRenderer.color;
        color.a = Mathf.Max(color.a - opacityDecrease, 0.1f);
        spriteRenderer.color = color; 

        if (brickLife <= 0)
        {
            FindObjectOfType<GameManager>().AddScore(points);
            FindObjectOfType<GameManager>().CheckLevelCompleted();
            DropPowerUp();
            Destroy(gameObject);
        }
    }

    private void DropPowerUp()
    {
        if (Random.value <= dropRate && powerUpPrefab != null)
        {
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        }
    }
}
