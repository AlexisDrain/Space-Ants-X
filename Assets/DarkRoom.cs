using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DarkRoom : MonoBehaviour {
    private Renderer objectRenderer;
    public Color setColor;

    void Start() {
        // If not set in the inspector, get the renderer component
        if (objectRenderer == null)
            objectRenderer = GetComponent<Renderer>();

    }
    private void LateUpdate() {
        objectRenderer.sharedMaterial.color = setColor;
    }
}