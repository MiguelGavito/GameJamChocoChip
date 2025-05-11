using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cargar escenas

public class ChangeScene : MonoBehaviour
{
    public int proximoNivel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                SceneManager.LoadScene(proximoNivel); // Cambiar a la escena indicada
            }
        }
    }
}