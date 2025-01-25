using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
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
