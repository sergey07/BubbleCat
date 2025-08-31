using UnityEngine;

public class BoilerForCatDiedScene : MonoBehaviour
{
    [Header("Sound Configuration")]
    // The gurgling sound of a cat falling into a boiler
    [SerializeField] public AudioClip _audioClipSplash;

    [Header("Components")]
    [SerializeField] private SplashSoundTrigger _splashSoundTrigger;
    [SerializeField] private AudioSource _audioSource;

    private bool _isSoundDone = false;

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        if (!_isSoundDone && _splashSoundTrigger.IsTriggered())
        {
            _isSoundDone = true;
            _audioSource.PlayOneShot(_audioClipSplash);
        }
    }

    public void Reset()
    {
        _isSoundDone = false;
    }
}
