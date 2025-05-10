using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // movimiento
    public float moveSpeed = 8f;
    public float jumpForce = 12f;

    // detecta piso
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool jumpPressed;

    private PlayerControls controls;

    // gravedad
    public float baseGravityScale = 3f;

    // para no poder salirse de la camara
    public Camera mainCamera;

    // respawns
    private Vector3 respawnPosition;
    public Vector2 roomSize = new Vector2(16f, 9f);
    private bool isAlive = true;
    public bool IsAlive => isAlive;

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    // si caes en una trampa de caida
    public float fallDeathY = -5.6f; 


    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Jump.performed += ctx => jumpPressed = true;
    }

    public void UpdateRespawnPoint(Vector2 roomCenter)
    {
        // calcular la esquina izquierda para respawn
        float left = roomCenter.x - (roomSize.x / 2f);

        // offset para que no empiece fuera del mapa
        respawnPosition = new Vector3(left + 1f, roomCenter.y, transform.position.z);
    }

    private void Respawn()
    {
        transform.position = respawnPosition;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = Vector2.zero;
        rb.rotation = 0f;
        rb.angularVelocity = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isAlive = true;
        jumpPressed = false;
        rb.constraints = RigidbodyConstraints2D.None;
    }

    public void Die()
    {
        isAlive = false;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic; // Freeze physics while "dead"

        // Agregar sonidos y animaciones
        Invoke(nameof(Respawn), 0.5f); // delay antes de respawnear
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // para respawn en el cuarto donde empieza
        respawnPosition = transform.position;
    if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        }

        jumpPressed = false; // se reinicia la flag al inicio de cada frame
        if (isAlive && transform.position.y < fallDeathY)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        // Movimiento vertical
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocityY);

        // checar si estas en el piso
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // agregar más gravedad cuando esta cayendo
        if (rb.linearVelocityY < 0)
        {
            rb.gravityScale = baseGravityScale * 1.5f;
        }
        else
        {
            rb.gravityScale = baseGravityScale;
        }

        // obtener la orilla de la camara
        float cameraLeftEdge = mainCamera.transform.position.x - (mainCamera.orthographicSize * mainCamera.aspect);

        // No dejar movimiento para salir de la camara en el lado izquierdo
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Max(clampedPosition.x, cameraLeftEdge);
        transform.position = clampedPosition;
    }
}
