using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicToggler : MonoBehaviour
{
    private TextMeshProUGUI _musicBtnText;
    private bool _isMusicOn = true;
    // public
    public Button musicBtn;
    public AudioSource audioSource;
    // interface
    public void MusicToggle()
    {
        if (_isMusicOn)
        {
            MusicMakeOff();
        }
        else
        {
            MusicMakeOn();
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _musicBtnText = musicBtn.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void MusicMakeOff()
    {
        if (audioSource != null)
        {
            audioSource.mute = true;
        }

        _isMusicOn = false;

        if (_musicBtnText != null)
        {
            _musicBtnText.text = "Music Off";
        }
    }
    private void MusicMakeOn()
    {
        audioSource.mute = false;
        _isMusicOn = true;
        _musicBtnText.text = "Music On";
    }
}
