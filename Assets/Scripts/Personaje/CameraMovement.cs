using UnityEngine;

public class OneWayRoomCamera : MonoBehaviour
{
    public Transform player;
    private PlayerMovement playerScript;
    public Vector2 roomSize = new Vector2(16f, 9f);
    public Vector2 cameraOffset = new Vector2(0f, 2f);
    public float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;
    private Vector2Int currentRoom;

    // para respawn
    public void SnapToRoom(Vector2 position)
    {
        int roomX = Mathf.FloorToInt(position.x / roomSize.x);
        int roomY = Mathf.FloorToInt(position.y / roomSize.y);
        currentRoom = new Vector2Int(roomX, roomY);

        float targetX = roomX * roomSize.x + roomSize.x / 2f + cameraOffset.x;
        float targetY = roomY * roomSize.y + roomSize.y / 2f + cameraOffset.y;
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }


    void Start()
    {
        playerScript = player.GetComponent<PlayerMovement>();

        Camera.main.orthographicSize = 7f; // Cambia el valor según lo que necesites
        // Calcular el cuarto inicial basado en la posición del jugador
        int roomX = Mathf.FloorToInt(player.position.x / roomSize.x);
        int roomY = Mathf.FloorToInt(player.position.y / roomSize.y);
        currentRoom = new Vector2Int(roomX, roomY);

        // Posicionar la cámara en el centro del cuarto inicial
        float targetX = roomX * roomSize.x + roomSize.x / 2f + cameraOffset.x;
        float targetY = roomY * roomSize.y + roomSize.y / 2f + cameraOffset.y;
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }

    void LateUpdate()
    {
        if (playerScript == null || !playerScript.IsAlive)
            return;

        // Movimiento vertical
        float cameraHeight = Camera.main.orthographicSize * 2f;
        float verticalThreshold = cameraHeight * 0.3f; // 30% de la altura visible
        float yOffsetFromCamera = player.position.y - transform.position.y;

        float targetY = transform.position.y;
        if (Mathf.Abs(yOffsetFromCamera) > verticalThreshold)
        {
            float direction = Mathf.Sign(yOffsetFromCamera);
            targetY = player.position.y - direction * verticalThreshold + cameraOffset.y;
        }

        // Movimiento horizontal (solo hacia la derecha)
        float targetX = Mathf.Max(transform.position.x, player.position.x + cameraOffset.x);

        // Calcular la posición objetivo de la cámara
        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

        // Suavizar el movimiento de la cámara
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}