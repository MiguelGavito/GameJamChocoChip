using JetBrains.Annotations;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    private float length, startpos;
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    public float parallaxEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Validar que el objeto tiene un SpriteRenderer.
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("El objeto no tiene un componente SpriteRenderer.");
            enabled = false; // Desactiva el script si no hay SpriteRenderer.
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
        length = spriteRenderer.bounds.size.x;
    }

    // Update is called once per frame
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
