using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boiler : MonoBehaviour
{
    // Звук булькания от попадания кота в котёл
    [SerializeField] public AudioClip _audioClipBulk;

    private Rigidbody2D _rb;
    private AudioSource _audioSource;
    private GameObject _soundTrigger;

    private PlayerStatus _playerStatus;
    private bool _isSoundTriggerAdded = false;
    private bool _isSoundDone = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

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
