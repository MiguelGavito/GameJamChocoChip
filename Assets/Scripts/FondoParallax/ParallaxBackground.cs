using NUnit.Framework.Constraints;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    public Transform cameraTransform;
    public float parallaxFactorX = 0.5f;
    public float parallaxFactorY = 0.3f;

    private Vector3 lastCameraPos;
    private float textureUnitSizeX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastCameraPos = cameraTransform.position;

        // Obtener el tamaño real de la textura en unidades
        Texture texture = GetComponent<Renderer>().material.mainTexture;
        float pixelsPerUnit = 16f; // <-- usa el valor que configuraste en la textura
        textureUnitSizeX = texture.width / pixelsPerUnit;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPos;
        transform.position += new Vector3(deltaMovement.x * parallaxFactorX, deltaMovement.y * parallaxFactorY, 0);
        lastCameraPos = cameraTransform.position;

        // Repetir si la cámara se aleja mucho horizontalmente
        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetX, transform.position.y, transform.position.z);
        }
    }
}
