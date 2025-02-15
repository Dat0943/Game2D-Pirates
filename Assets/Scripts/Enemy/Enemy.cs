using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float climbSpeed = 2f;
    [SerializeField] private float knockbackForce = 5f;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    bool isFlipped = false;
    bool isClimbing = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        rb.gravityScale = 0; 
        rb.velocity = new Vector2(0, climbSpeed); 
    }


    void Update()
    {
        if (rb.gravityScale == 0)  
        {
            rb.velocity = new Vector2(0, climbSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TagConsts.OBSTACLE_TAG)
        {
            Flip();
        }
    }

    void Flip()
    {
        if (transform.position.x >= 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

        transform.position = new Vector2(-transform.position.x, transform.position.y);
    }

    public void Knockback()
    {
        rb.gravityScale = 1f;
        rb.velocity = Vector2.zero;
        float knockbackDirection = transform.position.x > 0 ? 1f : -1f;
        rb.AddForce(new Vector2(knockbackDirection, 1f) * knockbackForce, ForceMode2D.Impulse);
    } 
}
