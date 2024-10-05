using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static Vector2 cameraBounds = new Vector3(40, 32);
    public static Vector2 cameraCoords = new Vector3(0, 0); // changed in cameraCoords
    public static Pool pool_LoudAudioSource;
    public static Transform playerTrans;
    public static Transform cameraDolly;

    public static UnityEvent playerRevive = new UnityEvent();
    public static UnityEvent playerChangeRoom = new UnityEvent();

    void Awake()
    {
        Cursor.visible = false;

        pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();
        playerTrans = GameObject.Find("Player").transform;
        cameraDolly = GameObject.Find("CameraDolly").transform;
    }
    public static void RevivePlayer() {
        print("revive player");
        playerRevive.Invoke();
    }
    public static Vector3 GetMousePositionOnFloor() {
        // Create a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        float distance = -ray.origin.y / ray.direction.y;
        Vector3 pointOnFloor = ray.origin + ray.direction * distance;

        return pointOnFloor;
    }
    // Update is called once per frame
    public static AudioSource SpawnLoudAudio(AudioClip newAudioClip, Vector2 pitch = new Vector2(), float newVolume = 1f) {

        float sfxPitch;
        if (pitch.x <= 0.1f) {
            sfxPitch = 1;
        } else {
            sfxPitch = Random.Range(pitch.x, pitch.y);
        }

        AudioSource audioObject = pool_LoudAudioSource.Spawn(new Vector3(0f, 0f, 0f)).GetComponent<AudioSource>();
        audioObject.GetComponent<AudioSource>().pitch = sfxPitch;
        audioObject.PlayWebGL(newAudioClip, newVolume);
        return audioObject;
        // audio object will set itself to inactive after done playing.
    }
}
