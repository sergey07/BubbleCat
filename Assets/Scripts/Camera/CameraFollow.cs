using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private float _cameraOffset = -10.0f;

    void LateUpdate()
    {
        PlayerStatus playerStatus = Player.Instance.GetPlayerStatus();

        if (playerStatus == PlayerStatus.InGame && _targetObject != null)
        {
            transform.position = new Vector3(_targetObject.transform.position.x, _targetObject.transform.position.y, _targetObject.transform.position.z + _cameraOffset);
        }
    }
}
