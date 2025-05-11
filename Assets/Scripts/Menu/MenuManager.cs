using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public AudioClip musicClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayMusic(musicClip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
