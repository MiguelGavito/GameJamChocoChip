using UnityEngine;

public class MusicaSelector : MonoBehaviour
{
    public AudioClip LevelMusic;

    public MusicManager musicManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayMusic(LevelMusic);
            MusicManager.Instance.SetVolume(0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
