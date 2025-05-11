using UnityEngine;

public class EnemyHorizontal : MonoBehaviour
{
    public float speed = 2f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    private bool movingRight = true;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool isGroundAhead = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        rb.linearVelocity = new Vector2(movingRight ? speed : -speed, rb.linearVelocityY);

        // Use OverlapCircle to check for ground
        bool groundDetected = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (!groundDetected && isGroundAhead)
        {
            Flip();
        }

        isGroundAhead = groundDetected;
    }

    void Flip()
    {
        movingRight = !movingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;

        // Flip groundCheck position (optional, keeps it in front)
        Vector3 gcLocalPos = groundCheck.localPosition;
        gcLocalPos.x *= -1;
        groundCheck.localPosition = gcLocalPos;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.collider.GetComponent<PlayerMovement>();
            if (player != null && player.IsAlive)
            {
                player.Die();
            }
        }
    }


}