using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static Pool pool_LoudAudioSource;
    public static Transform playerTrans;

    public static UnityEvent playerRevive;

    void Awake()
    {
        Cursor.visible = false;

        pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();
        playerTrans = GameObject.Find("Player").transform;
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
