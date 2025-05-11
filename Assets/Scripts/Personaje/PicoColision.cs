using UnityEngine;

public class PicoColision : MonoBehaviour
{

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static; // Se detiene al llegar al piso
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
        }
    }
   
}
