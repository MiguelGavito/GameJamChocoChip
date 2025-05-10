using UnityEngine;

public class ObstaculoCaida : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasHitGround = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Drop()
    {
        rb.bodyType = RigidbodyType2D.Dynamic; // empieza a caer
        rb.gravityScale = 4f; //más gravedad
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerMovement player = collision.collider.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.Die();
            }
        } else if (collision.collider.CompareTag("Ground"))
        {
            hasHitGround = true;
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static; // Stop moving after hitting the ground
        }
    }
}
