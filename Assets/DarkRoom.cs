using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRoom : MonoBehaviour {
    private Renderer objectRenderer;
    public Color setColor;
    public Color noColor;

    private FlamethrowerController flamethrower;
    void Start() {
        // If not set in the inspector, get the renderer component
        flamethrower = GameManager.playerGun.GetComponent<FlamethrowerController>();
        if (objectRenderer == null)
            objectRenderer = GetComponent<Renderer>();

    }
    private void LateUpdate() {
        if(flamethrower._isFiring) {
            objectRenderer.sharedMaterial.color = noColor;
        } else {
            objectRenderer.sharedMaterial.color = setColor;
        }
    }
}