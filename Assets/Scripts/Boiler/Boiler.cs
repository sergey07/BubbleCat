using UnityEngine;

public class Boiler : MonoBehaviour
{
    // The gurgling sound of a cat falling into a boiler
    [SerializeField] public AudioClip _audioClipSplash;

    [Header("Components")]
    [SerializeField] private SplashSoundTrigger _splashSoundTrigger;
    [SerializeField] private AudioSource _audioSource;

    private PlayerStatus _playerStatus;
    private bool _isSoundTriggerAdded = false;
    private bool _isSoundDone = false;
}
