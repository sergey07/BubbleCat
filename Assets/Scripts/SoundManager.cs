using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private bool _isMute = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Mute(!Progress.Instance.PlayerInfo.IsSoundOn);
    }

    public void Mute(bool isMute)
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        _isMute = isMute;
        Progress.Instance.PlayerInfo.IsSoundOn = !_isMute;

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = isMute;
        }
    }

    public bool IsMute()
    {
        return _isMute;
    }
}
