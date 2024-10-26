using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Rigidbody2D rigidBody2D;
    public Slider slider;
    public float moveSpeed = 25;
    private Vector2 startPosition;
    private bool sliderMoved = false;
    private bool isAutoMode = false; 

    // Power Up
    public GameObject bulletPrefab;
    public float shootingInterval = 0.5f;
    private Coroutine shootingCoroutine;

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.interpolation = RigidbodyInterpolation2D.Interpolate;
        startPosition = transform.position;
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void Update()
    {
        if (isAutoMode)
        {
            AutoMove(); 
        }
    }

    private void AutoMove()
    {
        Ball ball = FindObjectOfType<Ball>();
        if (ball != null)
        {
            Vector2 newPlayerPosition = new Vector2(ball.transform.position.x, transform.position.y);
            rigidBody2D.MovePosition(newPlayerPosition);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        float targetPositionX = value * (Screen.width / 100); 
        Vector2 targetPosition = new Vector2(targetPositionX, transform.position.y);
        rigidBody2D.position = Vector2.Lerp(rigidBody2D.position, targetPosition, Time.deltaTime * moveSpeed);

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

    public void ToggleAutoMode(Button button)
    {
        isAutoMode = !isAutoMode;
        sliderMoved = false;
        if (button != null)
        {
            button.GetComponentInChildren<Text>().text = isAutoMode ? "AUTO ON" : "AUTO OFF";
        }
        Debug.Log("Modo automático: " + (isAutoMode ? "Activado" : "Desactivado"));
    }
}
