using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerController : MonoBehaviour
{

    // public Transform crosshairTransform;
    public float defaultShootCountdown = 0.2f;
    public AudioClip clip_stopFlamethrower;

    public LineRenderer lineRenderer;
    public ParticleSystem flamethrowerParticleSystem;

    [Header("Read only")]
    public bool _isFiring = false;
    private float currentShootCountdown;
    private AudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }


    private void Update() {
        if (Input.GetButtonDown("Shoot")) {
            flamethrowerParticleSystem.Play();
            myAudioSource.PlayWebGL();
            _isFiring = true;
        }
        if (Input.GetButtonUp("Shoot")) {
            flamethrowerParticleSystem.Stop();
            myAudioSource.StopWebGL();
            GameManager.SpawnLoudAudio(clip_stopFlamethrower);
            _isFiring = false;
        }
    }
    void SpawnBullets() {
        Vector3 mousePosition = GameManager.GetMousePositionOnFloor(); // + new Vector3(0f, 1f, 0f);
        Vector3 direction = (mousePosition - GameManager.playerTrans.position).normalized;
        List<float> angles = new List<float> { -30f, -15f, 0f, 15f, 30f };

        for(int i = 0; i < angles.Count; i++) {
            GameObject obj = GameManager.pool_flamethrowerBullets.Spawn(transform.position);
            obj.GetComponent<Rigidbody>().position = transform.position;
            obj.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            Vector3 rotatedDirection = Quaternion.Euler(0f, angles[i], 0f) * direction;
            obj.GetComponent<BulletController>().SetDirection(rotatedDirection);
        }
    }
    // Update is called once per frame
    void FixedUpdate() {
        Vector3 mousePosition = GameManager.GetMousePositionOnFloor(); // + new Vector3(0f, 1f, 0f);
        Vector3 direction = (mousePosition - GameManager.playerTrans.position).normalized;

        if(currentShootCountdown > 0f) {
            currentShootCountdown -= Time.deltaTime;
        } else if (currentShootCountdown <= 0f && Input.GetButton("Shoot")) {
            currentShootCountdown = defaultShootCountdown;

            SpawnBullets();
        }


        // crosshairTransform.position = mousePosition + new Vector3(0f, 0.1f, 0f);

        // line to crosshair
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, mousePosition);

        // flamethrower particles direction
        Vector3 flatDirection = new Vector3(direction.x, 0f, direction.z);
        flamethrowerParticleSystem.transform.rotation = Quaternion.LookRotation(flatDirection);


    }
}
