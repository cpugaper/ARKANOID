using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Rigidbody2D rigidBody2D;
    private float inputValue;
    public float moveSpeed = 25;
    private Vector2 direction;
    Vector2 startPosition;

    // Power Up
    public GameObject bulletPrefab;
    public float shootingInterval = 0.5f;
    private Coroutine shootingCoroutine;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        startPosition = transform.position; 
    }

    void Update()
    {
        inputValue = Input.GetAxisRaw("Horizontal");

        if (inputValue == 1)
        {
            direction = Vector2.right;
        }
        else if (inputValue == -1)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.zero; 
        }

        rigidBody2D.AddForce(direction * moveSpeed * Time.deltaTime * 100);
    }

    public void ActivateShooting(float duration)
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
        }
        shootingCoroutine = StartCoroutine(Shoot(duration));
    }

    private IEnumerator Shoot(float duration)
    {
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ResetPlayer()
    {
        transform.position = startPosition;
        rigidBody2D.velocity = Vector2.zero; 
    }
}
