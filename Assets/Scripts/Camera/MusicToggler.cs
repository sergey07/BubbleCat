using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicToggler : MonoBehaviour
{
    private TextMeshProUGUI musicBtnText;
    private bool isMusicOn = true;
    // public
    public Button musicBtn;
    public AudioSource audioSource;
    // interface
    public void MusicToggle() {
        if (isMusicOn) {
            MusicMakeOff();
        } else {
            MusicMakeOn();
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        musicBtnText = musicBtn.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void MusicMakeOff() {
        audioSource.mute = true;
        isMusicOn = false;
        musicBtnText.text = "Music Off";
    }
    private void MusicMakeOn() {
        audioSource.mute = false;
        isMusicOn = true;
        musicBtnText.text = "Music On";
    }
}
