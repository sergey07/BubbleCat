// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private PauseCounter _pauseCounter;
    [SerializeField] private MusicToggler _musicToggler;

    private TextMeshProUGUI _zoomBtnText;
    private bool _isZoomBig = true;
    // public
    public Camera cam;
    public Button zoomBtn;

    public void Resume()
    {
        _pauseCounter.PauseToggle();
    }

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
        _pauseCounter.SpeedToggle();
    }


    public void Exit()
    {
        // Application.ExternalCall("location.reload");
    }
    // interface
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
    void Start()
    {
        cam = GetComponent<Camera>();
        _zoomBtnText = zoomBtn.GetComponentInChildren<TextMeshProUGUI>();
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
}
