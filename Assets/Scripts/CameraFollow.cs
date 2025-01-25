using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float cameraOffset = -10.0f;

    void LateUpdate()
    {
        transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z + cameraOffset);
    }
}
