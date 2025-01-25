using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraZoom : MonoBehaviour
{
    private TextMeshProUGUI zoomBtnText;
    private bool isZoomBig = true;
    // public
    public Camera cam;
    public Button zoomBtn;
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
        zoomBtnText = zoomBtn.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void ZoomMakeSmall() {
        // Set the size of the viewing volume you'd like the orthographic Camera to pick up
        cam.orthographicSize = 6.0f;
        isZoomBig = false;
        zoomBtnText.text = "Zoom x6";
    }
    private void ZoomMakeBig() {
        // Set the size of the viewing volume you'd like the orthographic Camera to pick up
        cam.orthographicSize = 8.0f;
        isZoomBig = true;
        zoomBtnText.text = "Zoom x8";
    }
}
