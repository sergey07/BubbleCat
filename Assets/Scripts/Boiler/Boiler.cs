using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boiler : MonoBehaviour
{
    // The gurgling sound of a cat falling into a boiler
    [SerializeField] public AudioClip _audioClipBulk;

    [Header("Components")]
    [SerializeField] private AudioSource _audioSource;

    private GameObject _soundTrigger;

    private PlayerStatus _playerStatus;
    private bool _isSoundTriggerAdded = false;
    private bool _isSoundDone = false;

    private void Update()
    {
        _playerStatus = Player.Instance.GetPlayerStatus();

        if (!_isSoundTriggerAdded && _playerStatus == PlayerStatus.InCatDiedScene)
        {
            _isSoundTriggerAdded = true;
            _soundTrigger = GameObject.Find("BulkSoundTrigger");
        }

        if (_soundTrigger != null)
        {
            if (!_isSoundDone && _soundTrigger.GetComponent<BulkSoundTrigger>().IsTriggered())
            {
                _isSoundDone = true;
                _audioSource.PlayOneShot(_audioClipBulk);
            }
        }
    }
}
