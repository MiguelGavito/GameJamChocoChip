using UnityEngine;

public class Caida : MonoBehaviour
{
    [SerializeField] private ObstaculoCaida obstaculo; // Se asigna en el inspector para simplicidad

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (obstaculo != null)
            {
                obstaculo.Drop();
                Destroy(gameObject); // Quitamos el trigger cuando ya cayó
            }
            else
            {
                Debug.LogWarning("Obstaculo no asignado!");
            }
        }
    }
}
