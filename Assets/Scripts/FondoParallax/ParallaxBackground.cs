using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float length, startpos;

    [SerializeField]
    private GameObject cam; // Cámara que se usará para calcular el efecto de parallax.

    [SerializeField]
    [Range(0f, 1f)] // Limita el rango del efecto de parallax entre 0 y 1.
    public float parallaxEffect;

    void Start()
    {
        // Validar que el objeto tiene un MeshRenderer.
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.LogError("El objeto no tiene un componente MeshRenderer.");
            enabled = false; // Desactiva el script si no hay MeshRenderer.
            return;
        }

        // Validar que la cámara está asignada.
        if (cam == null)
        {
            Debug.LogError("La cámara no está asignada en el script ParallaxBackground.");
            enabled = false; // Desactiva el script si no hay cámara.
            return;
        }

        startpos = transform.position.x;

        // Calcular la longitud del Quad basado en el tamaño del MeshRenderer.
        length = meshRenderer.bounds.size.x;
    }

    void FixedUpdate()
    {
        if (cam == null) return; // Evitar errores si la cámara no está asignada.

        // Calcular la distancia para el efecto de parallax.
        float dist = cam.transform.position.x * parallaxEffect;

        // Actualizar la posición del objeto.
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        // Ajustar la posición del fondo para que sea infinito.
        float distCam = cam.transform.position.x * (1 - parallaxEffect);

        if (distCam > startpos + length)
        {
            startpos += length;
        }
        else if (distCam < startpos - length)
        {
            startpos -= length;
        }
    }
}