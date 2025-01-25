using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;
using TMPro;

public class PauseCounter : MonoBehaviour
{
    private bool isPaused;
    // public
    public TextMeshProUGUI _timeText;
    // interface
    public void PauseToggle() {
        if (isPaused) {
            ResumeMake();
        } else {
            PauseMake();
        }
    }
    // start
    void Start() {
        PauseMake();
    }
    // Update
    void Update() {
        UpdateMaker();
    }
    private void UpdateMaker() {
        // DisplayTime(Time.time);
        CheckPauseButton();
    }
    private void DisplayTime(float timeToDisplay) {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        // Debug.Log(minutes+""+seconds);
        _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private void CheckPauseButton() {
        // И поменять на стандартную клавишу для разных платформ!!!
        if (Input.GetButtonDown("Jump")) {
            PauseToggle();
        }
    }
    private void ResumeMake() {
        // pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    private void PauseMake() {
        // pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
// https://t.me/natureModelSpb