using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float startTime = 60f; // poner cantidad de tiempo, por default 60 segundos

    private float timeRemaining;
    private bool isRunning = true; // para detener o pausar el timer

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeRemaining = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            timeRemaining -= Time.deltaTime;
            timeRemaining = Mathf.Max(timeRemaining, 0f); // Clamp at 0

            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (timeRemaining <= 0f)
            {
                isRunning = false;
                OnTimerEnd();
            }
        }
    }

    private void OnTimerEnd()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
}
