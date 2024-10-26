using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Rigidbody2D rigidBody2D;
    public Slider slider;
    public float moveSpeed = 25;
    Vector2 startPosition;
    private bool sliderMoved = false; 

    // Power Up
    public GameObject bulletPrefab;
    public float shootingInterval = 0.5f;
    private Coroutine shootingCoroutine;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        Vector2 newPosition = new Vector2(value * moveSpeed * Time.deltaTime, transform.position.y);
        rigidBody2D.MovePosition(newPosition);

        if (!sliderMoved)
        {
            sliderMoved = true;

            Ball ball = FindObjectOfType<Ball>();
            if (ball != null)
            {
                ball.LaunchBall();
            }
        }
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
        slider.value = 0;
        sliderMoved = false; 
    }
}
