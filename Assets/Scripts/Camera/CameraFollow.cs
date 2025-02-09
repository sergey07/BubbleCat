using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _cameraOffset = -10.0f;
    private GameObject _targetObject;

    private void Start()
    {
        _targetObject = Player.Instance.gameObject;
    }

    private void LateUpdate()
    {
        PlayerStatus playerStatus = Player.Instance.GetPlayerStatus();

        if (playerStatus == PlayerStatus.InGame && _targetObject != null)
        {
            transform.position = new Vector3(_targetObject.transform.position.x, _targetObject.transform.position.y, _targetObject.transform.position.z + _cameraOffset);
        }
    }
}
