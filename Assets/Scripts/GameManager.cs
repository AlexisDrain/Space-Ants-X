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
    public bool cheats = true;
    public static Day currentDay = Day.day1;
    public static int neededToKillCounter;
    public static Vector2 cameraBounds = new Vector3(40, 32);
    public static Vector2 cameraCoords = new Vector3(1, 0); // changed in cameraCoords
    public static Pool pool_LoudAudioSource;
    public static Pool pool_flamethrowerBullets;
    public static Transform playerRespawn;
    public static Transform playerTrans;
    public static Transform playerGun;
    public static Transform cameraDolly;
    public static Transform dayStartTransform;
    public static Transform sleepPodForPlayer;
    public static AudioSource music;
    public static AudioSource tiktokSound;
    public static Transform cutscene1;
    public static Transform cutscene2;
    public static Transform cutscene3;

    public static UnityEvent playerReviveEvent = new UnityEvent();
    public static UnityEvent changeDayEvent = new UnityEvent();
    public static UnityEvent playerChangeRoomEvent = new UnityEvent();
    public static UnityEvent changeKillCountEvent = new UnityEvent();

    void Awake()
    {
        Cursor.visible = false;

        pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();
        pool_flamethrowerBullets = transform.Find("Pool_FlamethrowerBullets").GetComponent<Pool>();
        playerRespawn = GameObject.Find("PlayerRespawn").transform;
        playerTrans = GameObject.Find("Player").transform;
        playerGun = GameObject.Find("Player/Gun").transform;
        cameraDolly = GameObject.Find("CameraDolly").transform;
        dayStartTransform = GameObject.Find("World/RoomPilot/DayStartPosition").transform;
        sleepPodForPlayer = GameObject.Find("World/RoomPilot/SleepPodPlayer").transform;
        music = transform.Find("Music").GetComponent<AudioSource>();
        tiktokSound = transform.Find("TicktokSound").GetComponent<AudioSource>();
        cutscene1 = GameObject.Find("Canvas/Cutscene1").transform;
        cutscene1.gameObject.SetActive(false);
        cutscene2 = GameObject.Find("Canvas/Cutscene2").transform;
        cutscene2.gameObject.SetActive(false);
        cutscene3 = GameObject.Find("Canvas/Cutscene3").transform;
        cutscene3.gameObject.SetActive(false);
    }
    private void Start() {
        Time.timeScale = 0f;
        playerGun.gameObject.SetActive(false);
    }
    public void Update() {
        if(cheats == true) {
            if (Input.GetButtonDown("Respawn")) {
                RevivePlayer();
            }
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha2)) {
                ChangeDay(2);
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha3)) {
                ChangeDay(3);
            }
        }
    }
    public static void ResumeGame() {
        Time.timeScale = 1f;
        if(music.isPlaying == false) {
            music.PlayWebGL();
            ChangeRoom();
            ChangeDay(1);
        }
    }
    public static void PickupFlamethrower() {
        playerGun.gameObject.SetActive(true);
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
        GameManager.playerTrans.GetComponent<Rigidbody>().position = dayStartTransform.position;
        GameManager.playerTrans.position = dayStartTransform.position;
        GameManager.playerTrans.GetComponent<PlayerController>().CannotMoveCutscene();
        playerRespawn.position = playerTrans.position;
        if (newDay == 1) {
            currentDay = Day.day1;
            cutscene1.gameObject.SetActive(true);
        } else if (newDay == 2) {
            currentDay = Day.day2;
            cutscene2.gameObject.SetActive(true);
        } else if (newDay == 3) {
            currentDay = Day.day3;
            cutscene3.gameObject.SetActive(true);
        }
        tiktokSound.PlayWebGL();
        sleepPodForPlayer.GetComponent<TriggerWait>().StartCoroutine(); // sets the colliders to none
        sleepPodForPlayer.GetComponent<Animator>().SetTrigger("Open");
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
    public void PrintMessage(string message) {
        print(message);
    }
}
