using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRoom : MonoBehaviour {
    private Renderer objectRenderer;
    public Color setColor;

    public void ChangeColor() {
        objectRenderer.material.color = setColor;
    }
    void Start() {
        // If not set in the inspector, get the renderer component
        if (objectRenderer == null)
            objectRenderer = GetComponent<Renderer>();

        // Change the material's color to red
        objectRenderer.material.color = Color.red;
    }
}