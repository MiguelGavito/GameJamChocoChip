using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;

    public AudioClip currentClip;

    [Range(0f, 1f)]
    public float volume = 1f;
    public bool isMuted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        UpdateVolume();
    }


    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return; // No recargar si ya se está tocando

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        UpdateVolume();
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        audioSource.volume = isMuted ? 0 : volume;
    }

}
