using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClip _audioClipFinishLevel;

    private AudioSource _audioSource;
    private bool _isMute = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
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
//        Progress.Instance.PlayerInfo.IsSoundOn = !_isMute;
//#if UNITY_WEBGL
//        Progress.Instance.Save();
//#endif

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = isMute;
        }
    }

    public bool IsMute()
    {
        return _isMute;
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
