using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCommon : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    public Camera getMainCamera()
    {
        return _mainCamera;
    }
}
