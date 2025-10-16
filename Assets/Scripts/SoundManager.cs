using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _audioSourceLoop;

    [SerializeField] private AudioClip _audioClipMainMusic;
    [SerializeField] private AudioClip _audioClipFinishLevel;

    private bool _isMute = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Init()
    {
        Mute(!Progress.Instance.PlayerInfo.IsSoundOn);
    }

    public void Mute(bool isMute)
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        _isMute = isMute;

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = isMute;
        }
    }

    public bool IsMute()
    {
        return _isMute;
    }

    public void PlayMainMusic()
    {
        _audioSourceLoop.Play();
    }

    public void StopMainMusic()
    {
        _audioSourceLoop.Stop();
    }

    public void PlayLevelFinishMusic()
    {
        _audioSource.PlayOneShot(_audioClipFinishLevel);
    }

    public float GetLevelFinishMusicDuration()
    {
        return _audioClipFinishLevel.length;
    }
}
