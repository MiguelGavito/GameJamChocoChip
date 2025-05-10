using UnityEngine;

public class OneWayRoomCamera : MonoBehaviour
{
    public Transform player;
    PlayerMovement playerScript;
    public Vector2 roomSize = new Vector2(16f, 9f);
    public Vector2 cameraOffset = new Vector2(0f, 2f);
    public float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;
    private float maxCameraX; // detecta la posición en x a la derecha maxima
    
    private Vector2Int currentRoom; // detecta el cuarto en el que estas


    void Start()
    {
        maxCameraX = transform.position.x;
        playerScript = player.GetComponent<PlayerMovement>();
    }

    // IMPLEMENTACION ANTERIOR NO UTILIZAR
    // void LateUpdate()
    // {
    //     Vector2 playerPos = player.position;

    //     // Determine which room the player is in
    //     float targetRoomX = Mathf.Floor(playerPos.x / roomSize.x) * roomSize.x + roomSize.x / 2f;
    //     float targetRoomY = Mathf.Floor(playerPos.y / roomSize.y) * roomSize.y + roomSize.y / 2f;

    //     // Apply offset
    //     float targetX = Mathf.Max(maxCameraX, targetRoomX + cameraOffset.x); // Prevent moving left
    //     float targetY = targetRoomY + cameraOffset.y;

    //     Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

    //     transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

    //     // Update the furthest X position the camera has reached
    //     maxCameraX = transform.position.x;
    // }

        void LateUpdate()
    {
        Vector2 playerPos = player.position;

        // obtener los indices del cuarto
        float roomWidth = roomSize.x;
        float roomHeight = roomSize.y;

        int roomX = Mathf.FloorToInt(playerPos.x / roomWidth);
        int roomY = Mathf.FloorToInt(playerPos.y / roomHeight);
        Vector2Int newRoom = new Vector2Int(roomX, roomY);

        // calcular el centro de cada cuarto
        float targetRoomX = roomX * roomWidth + roomWidth / 2f;
        float targetRoomY = roomY * roomHeight + roomHeight / 2f;

        // detectar si se cambia de cuarto
        PlayerMovement playerScript = player.GetComponent<PlayerMovement>();
        if (newRoom != currentRoom && playerScript != null && playerScript.IsAlive)
        {
            currentRoom = newRoom;
            playerScript.UpdateRespawnPoint(new Vector2(targetRoomX, targetRoomY));
        }
        
        // por si hay un error o el jugador no esta vivo, no se actualiza la camara
        if (playerScript == null || !playerScript.IsAlive)
        return;

        // movimiento al siguiente "cuarto"
        float targetX = Mathf.Max(transform.position.x, targetRoomX + cameraOffset.x); // For one-way right
        float targetY = targetRoomY + cameraOffset.y;

        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // update a la camara si es necesaria
        maxCameraX = transform.position.x;
    }
}
