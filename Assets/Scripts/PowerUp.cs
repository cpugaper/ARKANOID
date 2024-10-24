using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float duration = 5f; 
    public float fallSpeed = 2f;

    public void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().ActivateShooting(duration);
            Destroy(gameObject); 
        }
        else if (collision.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
    }
}
