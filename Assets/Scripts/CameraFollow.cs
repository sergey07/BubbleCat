using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private float cameraOffset = -10.0f;
    public Camera cam;
    private bool isZoomBig = true;

    // interface
    public void ZoomCamerToggle() {
        if (isZoomBig) {
            ZoomMakeSmall();
        } else {
            ZoomMakeBig();
        }
    }
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    void LateUpdate()
    {
        if (targetObject != null)
        {
            transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z + cameraOffset);
        }
    }
    private void ZoomMakeSmall() {
        // Set the size of the viewing volume you'd like the orthographic Camera to pick up
        cam.orthographicSize = 6.0f;
        isZoomBig = false;
    }
    private void ZoomMakeBig() {
        // Set the size of the viewing volume you'd like the orthographic Camera to pick up
        cam.orthographicSize = 8.0f;
        isZoomBig = true;
    }
}
