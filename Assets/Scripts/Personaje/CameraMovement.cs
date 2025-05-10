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

        // codigo para nueva implementación

        float roomWidth = roomSize.x;
        float roomHeight = roomSize.y;

        // poner el cuarto inicial en la posición inicial del jugador
        int roomX = Mathf.FloorToInt(player.position.x / roomWidth);
        int roomY = Mathf.FloorToInt(player.position.y / roomHeight);
        currentRoom = new Vector2Int(roomX, roomY);

        // mover camara a donde inicia
        float targetX = roomX * roomWidth + roomWidth / 2f + cameraOffset.x;
        float targetY = roomY * roomHeight + roomHeight / 2f + cameraOffset.y;
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }

    // IMPLEMENTACION ANTERIOR, SE MUEVE CONTINUAMENTE
    // void LateUpdate()
    // {
    //     Vector2 playerPos = player.position;

    //     // obtener los indices del cuarto
    //     float roomWidth = roomSize.x;
    //     float roomHeight = roomSize.y;

    //     int roomX = Mathf.FloorToInt(playerPos.x / roomWidth);
    //     int roomY = Mathf.FloorToInt(playerPos.y / roomHeight);
    //     Vector2Int newRoom = new Vector2Int(roomX, roomY);

    //     // calcular el centro de cada cuarto
    //     float targetRoomX = roomX * roomWidth + roomWidth / 2f;
    //     float targetRoomY = roomY * roomHeight + roomHeight / 2f;

    //     // detectar si se cambia de cuarto
    //     PlayerMovement playerScript = player.GetComponent<PlayerMovement>();
    //     if (newRoom != currentRoom && playerScript != null && playerScript.IsAlive)
    //     {
    //         currentRoom = newRoom;
    //         playerScript.UpdateRespawnPoint(new Vector2(targetRoomX, targetRoomY));
    //     }
        
    //     // por si hay un error o el jugador no esta vivo, no se actualiza la camara
    //     if (playerScript == null || !playerScript.IsAlive)
    //     return;

    //     // movimiento al siguiente "cuarto"
    //     float targetX = Mathf.Max(transform.position.x, targetRoomX + cameraOffset.x); // For one-way right
    //     float targetY = targetRoomY + cameraOffset.y;

    //     Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);
    //     transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

    //     // update a la camara si es necesaria
    //     maxCameraX = transform.position.x;
    // }

    void LateUpdate()
    {
        if (playerScript == null || !playerScript.IsAlive)
            return;

        float roomWidth = roomSize.x;
        float roomHeight = roomSize.y;

        // obtener el centro/origen del cuarto
        Vector2 roomOrigin = new Vector2(currentRoom.x * roomWidth, currentRoom.y * roomHeight);
        float roomRightEdge = roomOrigin.x + roomWidth;

        // si el personaje llega a la orilla de la pantalla mover a la derecha
        if (player.position.x > roomRightEdge)
        {
            currentRoom.x += 1; // camara solo se puede mover a la derecha
            Vector2 newRoomCenter = new Vector2(currentRoom.x * roomWidth + roomWidth / 2f, currentRoom.y * roomHeight + roomHeight / 2f);
            playerScript.UpdateRespawnPoint(newRoomCenter);
        }

        // obtener centro de la pantalla a la que nos queremos mover
        float targetX = currentRoom.x * roomWidth + roomWidth / 2f + cameraOffset.x;
        float targetY = currentRoom.y * roomHeight + roomHeight / 2f + cameraOffset.y;

        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
