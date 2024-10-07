using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectRatioForcer : MonoBehaviour {
    public float targetAspectRatio = 16f / 9f; // Set this to your desired aspect ratio
    private Camera cam;

    void Start() {
        cam = GetComponent<Camera>();
        if (cam == null) {
            Debug.LogError("CameraAspectRatioForcer: No camera component found!");
            return;
        }

        UpdateCameraRect();
    }

    void UpdateCameraRect() {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspectRatio;

        if (scaleHeight < 1f) {
            Rect rect = cam.rect;
            rect.width = 1f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1f - scaleHeight) / 2f;
            cam.rect = rect;
        } else {
            float scaleWidth = 1f / scaleHeight;
            Rect rect = cam.rect;
            rect.width = scaleWidth;
            rect.height = 1f;
            rect.x = (1f - scaleWidth) / 2f;
            rect.y = 0;
            cam.rect = rect;
        }
    }

    void OnPreCull() {
        UpdateCameraRect();
    }
}