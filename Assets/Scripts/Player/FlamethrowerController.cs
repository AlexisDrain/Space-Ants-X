using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerController : MonoBehaviour
{
    public float projectionDistance = 10f; // Distance to project, adjust as needed

    // public Transform crosshairTransform;
    public AudioClip clip_stopFlamethrower;

    public LineRenderer lineRenderer;
    public ParticleSystem flamethrowerParticleSystem;

    private AudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    /*
    Vector3 GetMouseWorldPosition() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = projectionDistance; // Set this to the distance you want to project to
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    */
    private void Update() {
        if (Input.GetButtonDown("Shoot")) {
            flamethrowerParticleSystem.Play();
            myAudioSource.PlayWebGL();
        }
        if (Input.GetButtonUp("Shoot")) {
            flamethrowerParticleSystem.Stop();
            myAudioSource.StopWebGL();
            GameManager.SpawnLoudAudio(clip_stopFlamethrower);
        }
    }
    // Update is called once per frame
    void FixedUpdate() {
        Vector3 mousePosition = GameManager.GetMousePositionOnFloor(); // + new Vector3(0f, 1f, 0f);

        // crosshairTransform.position = mousePosition + new Vector3(0f, 0.1f, 0f);

        // line to crosshair
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, mousePosition);

        // flamethrower particles direction
        Vector3 direction = (mousePosition - GameManager.playerTrans.position).normalized;
        Vector3 flatDirection = new Vector3(direction.x, 0f, direction.z);
        flamethrowerParticleSystem.transform.rotation = Quaternion.LookRotation(flatDirection);


    }
}
