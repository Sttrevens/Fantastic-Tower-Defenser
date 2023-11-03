using UnityEngine;
using TMPro;  // Required for TextMeshPro

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText; // Drag your TextMeshProUGUI component here in the inspector
    public float timeRemaining = 60; // 60 seconds countdown
    private float startTime;

    private bool timerIsRunning = true;
    private bool gameSucceeded = false;

    public GameObject successPanel;

    public SequentialFade[] enemyLines;

    private void Start()
    {
        Time.timeScale = 1f;
        UpdateTimerDisplay();
        successPanel.SetActive(false);

        startTime = timeRemaining;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; // Decrease the remaining time
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false; // Stop the timer
                GameSuccess();
            }
        }

        Progression();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);  // Calculate minutes
        int seconds = Mathf.FloorToInt(timeRemaining % 60);  // Calculate seconds

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Update the display
    }

    private void GameSuccess()
    {
        if (!gameSucceeded)  // Make sure this only happens once
        {
            gameSucceeded = true;
            timerText.text = "Success!";
            successPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void Progression()
    {
        if (timeRemaining <= startTime / 2)
        {
            foreach (SequentialFade enemyLine in enemyLines)
            {
                enemyLine.spawnRangefrom = enemyLine.spawnRangefrom / 2;
                enemyLine.spawnRangeto = enemyLine.spawnRangeto / 2;
            }
        }

        else if (timeRemaining <= startTime / 4)
        {
            foreach (SequentialFade enemyLine in enemyLines)
            {
                enemyLine.spawnRangefrom = enemyLine.spawnRangefrom / 4;
                enemyLine.spawnRangeto = enemyLine.spawnRangeto / 4;
            }
        }
    }
}
