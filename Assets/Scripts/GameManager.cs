using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum Day {
    day1,
    day2,
    day3
}
public class GameManager : MonoBehaviour
{
    public static Transform playerRespawn;
    public static Day currentDay = Day.day1;
    public static int neededToKillCounter;
    public static Vector2 cameraBounds = new Vector3(40, 32);
    public static Vector2 cameraCoords = new Vector3(1, 0); // changed in cameraCoords
    public static Pool pool_LoudAudioSource;
    public static Pool pool_flamethrowerBullets;
    public static Transform playerTrans;
    public static Transform cameraDolly;

    public static UnityEvent playerReviveEvent = new UnityEvent();
    public static UnityEvent changeDayEvent = new UnityEvent();
    public static UnityEvent playerChangeRoomEvent = new UnityEvent();
    public static UnityEvent changeKillCountEvent = new UnityEvent();

    void Awake()
    {
        Cursor.visible = false;

        pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();
        pool_flamethrowerBullets = transform.Find("Pool_FlamethrowerBullets").GetComponent<Pool>();
        playerTrans = GameObject.Find("Player").transform;
        cameraDolly = GameObject.Find("CameraDolly").transform;
        playerRespawn = GameObject.Find("PlayerRespawn").transform;

    }
    private void Start() {
        ChangeRoom();
        ChangeDay(1);
    }
    public void Update() {
        if (Input.GetButtonDown("Respawn")) {
            RevivePlayer();
        }
    }
    public static void ChangeRoom() {
        playerRespawn.position = playerTrans.position;
        playerChangeRoomEvent.Invoke();
    }
    public static void RevivePlayer() {
        print("revive player");
        playerTrans.position = playerRespawn.position;
        playerReviveEvent.Invoke();
    }
    public static void ChangeDay(int newDay) {
        if(newDay == 1) {
            currentDay = Day.day1;
        } else if (newDay == 2) {
            currentDay = Day.day2;
        } else if (newDay == 3) {
            currentDay = Day.day3;
        }
        changeDayEvent.Invoke();
    }
    public static void UpdateKillCount(int newValue) {
        neededToKillCounter += newValue;
        changeKillCountEvent.Invoke();
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
