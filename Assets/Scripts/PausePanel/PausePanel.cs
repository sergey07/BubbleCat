// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private MusicToggler _musicToggler;
    
    // private bool _isPaused = false;
    private float _currentSpeed = 1.0f;
    private int _difficultyLvl = 1;

    [SerializeField] private TextMeshProUGUI _difficultyBtnText;

    [SerializeField] private float _easyDifficultyLvl = 1.0f;
    [SerializeField] private float _midDifficultyLvl = 1.5f;
    [SerializeField] private float _hardDifficultyLvl = 2.0f;

    //[SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Button difficultyBtn;

    private TextMeshProUGUI _zoomBtnText;
    private bool _isZoomBig = true;
    // public
    public Camera cam;
    public Button zoomBtn;

    void Start()
    {
        // _zoomBtnText = zoomBtn.GetComponentInChildren<TextMeshProUGUI>();
    }

    // public void Resume()
    // {
    //     ResumeMake();
    // }

    public void Restart()
    {
        GameManager.Instance.RestartScene();
    }

    public void SoundToggler()
    {
        _musicToggler.MusicToggle();
    }

    public void ChangeDifficulty()
    {
        if (_difficultyLvl == 1)
        {
            MidDifficultyLvl();
        }
        else if (_difficultyLvl == 2)
        {
            HardDifficultyLvl();
        }
        else if (_difficultyLvl == 3)
        {
            EasyDifficultyLvl();
        }
    }

    public void Exit()
    {
        // Application.ExternalCall("location.reload");
    }

    // public void PauseToggle()
    // {
    //     if (_isPaused)
    //     {
    //         ResumeMake();
    //     }
    //     else
    //     {
    //         PauseMake();
    //     }
    // }

    // private void ResumeMake()
    // {
    //     //Debug.Log("ResumeMake");
    //     if (pausePanel != null)
    //     {
    //         pausePanel.SetActive(false);
    //     }
    //     Time.timeScale = _currentSpeed;
    //     _isPaused = false;
    // }

    // private void PauseMake()
    // {
    //     pausePanel.SetActive(true);
    //     Time.timeScale = 0.0f;
    //     _isPaused = true;
    // }

    private void EasyDifficultyLvl()
    {
        _currentSpeed = _easyDifficultyLvl;
        _difficultyLvl = 1;
        _difficultyBtnText.text = "Easy";
    }

    private void MidDifficultyLvl()
    {
        _currentSpeed = _midDifficultyLvl;
        _difficultyLvl = 2;
        _difficultyBtnText.text = "Medium";
    }

    private void HardDifficultyLvl()
    {
        _currentSpeed = _hardDifficultyLvl;
        _difficultyLvl = 3;
        _difficultyBtnText.text = "Hard";
    }
    
    public void ChangeScale()
    {
        if (_isZoomBig)
        {
            ZoomMakeSmall();
        }
        else
        {
            ZoomMakeBig();
        }
    }

    private void ZoomMakeSmall()
    {
        // Set the size of the viewing volume you'd like the orthographic Camera to pick up
        cam.orthographicSize = 6.0f;
        _isZoomBig = false;
        _zoomBtnText.text = "Zoom x6";
    }
    private void ZoomMakeBig()
    {
        // Set the size of the viewing volume you'd like the orthographic Camera to pick up
        cam.orthographicSize = 8.0f;
        _isZoomBig = true;
        _zoomBtnText.text = "Zoom x8";
    }

    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        // Debug.Log(minutes+""+seconds);
        //timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
