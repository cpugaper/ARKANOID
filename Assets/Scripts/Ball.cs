using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody2D rigidBody2D;
    public float speed = 200;
    private Vector2 velocity;
    private Vector2 startPosition;
    private float maxSpeed = 7;
    private float launchTimer = 2.0f;
    public float speedMultiplier = 1.05f;
    private bool gameStarted = false;

    public AudioSource audioSource;
    public AudioClip playerSound, brickSound, wallSound, deadZoneSound;

    //screen boundary 
    private float screenTop, screenBottom, screenLeft, screenRight;

    void Start()
    {
        startPosition = transform.position;
        ResetBall();
    }

    void Update()
    {
        if (!gameStarted)
        {
            launchTimer -= Time.deltaTime;
            if (launchTimer <= 0)
            {
                LaunchBall();
            }
        }
    }

    public void LaunchBall()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            velocity.x = Random.Range(-1f, 1f);
            velocity.y = 1;
            rigidBody2D.AddForce(velocity.normalized * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        rigidBody2D.velocity *= speedMultiplier;

        if (rigidBody2D.velocity.magnitude > maxSpeed)
        {
            rigidBody2D.velocity = rigidBody2D.velocity.normalized * maxSpeed;
        }

        if (Mathf.Abs(rigidBody2D.velocity.x) < 0.5f)
        {
            rigidBody2D.velocity = new Vector2(0.5f * Mathf.Sign(rigidBody2D.velocity.x),
                 rigidBody2D.velocity.y).normalized * rigidBody2D.velocity.magnitude;
        }

        if (Mathf.Abs(rigidBody2D.velocity.y) < 0.5f)
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x,
                0.5f * Mathf.Sign(rigidBody2D.velocity.y)).normalized * rigidBody2D.velocity.magnitude;
        }

        if (collision.gameObject.CompareTag("DeadZone"))
        {
            audioSource.clip = deadZoneSound;
            audioSource.Play();
            FindObjectOfType<GameManager>().LoseHealth();
        }

        if (collision.transform.CompareTag("Wall"))
        {
            audioSource.clip = wallSound;
            audioSource.Play();
        }

        if (collision.gameObject.GetComponent<Player>())
        {
            audioSource.clip = playerSound;
            audioSource.Play();
        }

        if (collision.gameObject.GetComponent<Brick>())
        {
            audioSource.clip = playerSound;
            audioSource.Play();
        }
    }

    public void ResetBall()
    {
        transform.position = startPosition;
        rigidBody2D.velocity = Vector2.zero;
        velocity = Vector2.zero;
        gameStarted = false;
        launchTimer = 2.0f; 
    }
}
