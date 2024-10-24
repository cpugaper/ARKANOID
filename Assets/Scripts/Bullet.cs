using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    public void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * speed; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Brick"))
        {
            collision.GetComponent<Brick>().TakeDamage();
            Destroy(gameObject);
        }
        
        if (collision.CompareTag("Wall") || collision.CompareTag("DeadZone"))
        {
            Destroy(gameObject);
        }
    }
}
