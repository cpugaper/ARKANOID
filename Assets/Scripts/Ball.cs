using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody2D rigidBody2D;
    public float speed = 200;
    private Vector2 velocity;
    private Vector2 startPosition;
    private float maxSpeed = 10; 

    public float speedMultiplier = 1.05f;

    private bool gameStarted = false;

    void Start()
    {
        startPosition = transform.position;
        ResetBall(); 
    }

    void Update()
    {
        if (!gameStarted)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                gameStarted = true;
                LaunchBall();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        rigidBody2D.velocity *= speedMultiplier;

        if (rigidBody2D.velocity.magnitude > maxSpeed)
        {
            rigidBody2D.velocity = rigidBody2D.velocity.normalized * maxSpeed;
        }

        if (collision.gameObject.CompareTag("DeadZone"))
        {
            FindObjectOfType<GameManager>().LoseHealth(); 
        }
    }

    public void ResetBall()
    {
        transform.position = startPosition;
        rigidBody2D.velocity = Vector2.zero;
        velocity = Vector2.zero;
        gameStarted = false; 
    }

    private void LaunchBall()
    {
        velocity.x = Random.Range(-1f, 1f);
        velocity.y = 1;
        rigidBody2D.AddForce(velocity.normalized * speed);
    }
}
