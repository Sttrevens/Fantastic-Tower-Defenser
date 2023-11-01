using UnityEngine;
using TMPro;  // Required for TextMeshPro

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText; // Drag your TextMeshProUGUI component here in the inspector
    public float timeRemaining = 60; // 60 seconds countdown

    private bool timerIsRunning = true;
    private bool gameSucceeded = false;

    public GameObject successPanel;

    private void Start()
    {
        UpdateTimerDisplay();
        successPanel.SetActive(false);
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
        }
    }
}
