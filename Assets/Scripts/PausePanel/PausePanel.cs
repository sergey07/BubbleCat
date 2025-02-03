using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private PauseCounter _pauseCounter;
    [SerializeField] private MusicToggler _musicToggler;
    [SerializeField] private CameraZoom _cameraZoom;

    public void Resume()
    {
        _pauseCounter.PauseToggle();
    }

    public void Restart()
    {
        _pauseCounter.RestartLevel();
    }

    public void SoundToggler()
    {
        _musicToggler.MusicToggle();
    }

    public void ChangeDifficulty()
    {
        _pauseCounter.SpeedToggle();
    }

    public void ChangeScale()
    {
        _cameraZoom.ZoomCamerToggle();
    }

    public void Exit()
    {

    }
}
