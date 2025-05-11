using JetBrains.Annotations;
using UnityEngine;

public class Fondo : MonoBehaviour
{
    private float length, startpos;
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    public float parallaxEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        float distCam = cam.transform.position.x * (1 - parallaxEffect);

        if (distCam > startpos + length) startpos += length;
        else if (distCam < startpos - length) startpos -= length;
    }
}
