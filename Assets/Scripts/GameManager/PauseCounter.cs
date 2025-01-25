using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseCounter : MonoBehaviour
{
    private bool isPaused = false;
    private float m_currentSpeed = 1.0f;
    private int m_diffLvl = 1;
    private TextMeshProUGUI diffBtnText;
    // public
    [SerializeField] private float easyDiffLvl = 1.0f;
    [SerializeField] private float midDiffLvl = 1.5f;
    [SerializeField] private float hardDiffLvl = 2.0f;
    public TextMeshProUGUI _timeText;
    public Button diffBtn;
    public GameObject pausePanel; // Принимаем объект панели паузы, чтобы ее скрыть и показать
    // interface
    public void PauseToggle() {
        if (isPaused) {
            ResumeMake();
        } else {
            PauseMake();
        }
    }
    public void SpeedToggle() {
        if (m_diffLvl == 1) {
            MidDiffLvl();
        } else if (m_diffLvl == 2) {
            HardDiffLvl();
        } else if (m_diffLvl == 3) {
            EasyDiffLvl();
        }
    }
    // start
    void Start() {
        //PauseMake();
        ResumeMake();
        diffBtnText = diffBtn.GetComponentInChildren<TextMeshProUGUI>();
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
        if (Input.GetKeyDown(KeyCode.Escape)) { // KeyCode.Y
            PauseToggle();
        }
    }
    private void ResumeMake() {
        pausePanel.SetActive(false);
        Time.timeScale = m_currentSpeed;
        isPaused = false;
    }
    private void PauseMake() {
        pausePanel.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }
    private void EasyDiffLvl() {
        m_currentSpeed = easyDiffLvl;
        m_diffLvl = 1;
        diffBtnText.text = "Easy";
    }
    private void MidDiffLvl() {
        m_currentSpeed = midDiffLvl;
        m_diffLvl = 2;
        diffBtnText.text = "Medium";
    }
    private void HardDiffLvl() {
        m_currentSpeed = hardDiffLvl;
        m_diffLvl = 3;
        diffBtnText.text = "Hard";
    }
}
// https://t.me/natureModelSpb