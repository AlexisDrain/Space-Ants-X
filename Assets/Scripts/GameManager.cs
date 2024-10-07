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
    public static bool hasStartedGame = false;
    public static bool playerIsDead = false;
    
    // self destructing
    public static bool isSelfDestructing = false;
    public static float defaultSelfDestructionTimer = 140f;
    public static float currentSelfDestructionTimer = 140f;

    public static Day currentDay = Day.day1;
    public static int neededToKillCounter;
    public static int defaultNeededToKill1 = 55;
    public static int defaultNeededToKill2 = 130;
    public static Vector2 cameraBounds = new Vector3(40, 32);
    public static Vector2 cameraCoords = new Vector3(1, 0); // changed in cameraCoords

    public static GameObject gameObjectManager;
    public static Pool pool_LoudAudioSource;
    public static Pool pool_flamethrowerBullets;
    public static Pool pool_antBullets;
    public static Pool pool_antDeadbodies;
    public static Pool pool_antBigDeadbodies;
    public static Transform playerRespawn;
    public static Transform playerTrans;
    public static Transform playerGun;
    public static Transform cameraDolly;
    public static Transform dayStartTransform;
    public static Transform sleepPodForPlayer;
    public static AudioSource music1;
    public static AudioSource music2;
    public static AudioSource music3;
    public static AudioSource tiktokSound;
    public static GameObject menus;
    public static Transform alarmImage;
    public static Transform selfDestructImage;
    public static Transform cutscene1;
    public static Transform cutscene2;
    public static Transform cutscene3;
    public static Transform pressRText;

    public static UnityEvent playerReviveEvent = new UnityEvent();
    public static UnityEvent changeDayEvent = new UnityEvent();
    public static UnityEvent playerChangeRoomEvent = new UnityEvent();
    public static UnityEvent changeKillCountEvent = new UnityEvent();

    void Awake()
    {
        gameObjectManager = gameObject;
        pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();
        pool_flamethrowerBullets = transform.Find("Pool_FlamethrowerBullets").GetComponent<Pool>();
        pool_antBullets = transform.Find("Pool_AntBullets").GetComponent<Pool>();
        pool_antDeadbodies = transform.Find("Pool_AntDeadbodies").GetComponent<Pool>();
        pool_antBigDeadbodies = transform.Find("Pool_AntBigDeadbodies").GetComponent<Pool>();
        playerRespawn = GameObject.Find("PlayerRespawn").transform;
        playerTrans = GameObject.Find("Player").transform;
        playerGun = GameObject.Find("Player/Gun").transform;
        cameraDolly = GameObject.Find("CameraDolly").transform;
        dayStartTransform = GameObject.Find("World/RoomPilot/DayStartPosition").transform;
        sleepPodForPlayer = GameObject.Find("World/RoomPilot/SleepPodPlayer").transform;
        music1 = transform.Find("Music1").GetComponent<AudioSource>();
        music2 = transform.Find("Music2").GetComponent<AudioSource>();
        music3 = transform.Find("Music3").GetComponent<AudioSource>();
        menus = GameObject.Find("Canvas/Menus");
        tiktokSound = transform.Find("TicktokSound").GetComponent<AudioSource>();
        alarmImage = GameObject.Find("Canvas/AlarmImage").transform;
        alarmImage.gameObject.SetActive(false);
        selfDestructImage = GameObject.Find("Canvas/SelfDestructImage").transform;
        selfDestructImage.gameObject.SetActive(false);
        cutscene1 = GameObject.Find("Canvas/Cutscene1").transform;
        cutscene1.gameObject.SetActive(false);
        cutscene2 = GameObject.Find("Canvas/Cutscene2").transform;
        cutscene2.gameObject.SetActive(false);
        cutscene3 = GameObject.Find("Canvas/Cutscene3").transform;
        cutscene3.gameObject.SetActive(false);
        pressRText = GameObject.Find("Canvas/PressR").transform;
        pressRText.gameObject.SetActive(false);
    }
    private void Start() {
        Cursor.visible = false;
        Time.timeScale = 0f;
        playerGun.gameObject.SetActive(false);
        hasStartedGame = false;
    }
    public void FixedUpdate() {
        if(currentDay == Day.day3) {
            if(currentSelfDestructionTimer >= 0) {
                currentSelfDestructionTimer -= Time.deltaTime;
            }
            else if(currentSelfDestructionTimer <= 0 && isSelfDestructing == false) {
                isSelfDestructing = true;
                SelfDestruct();
            }
        }
    }
    public static void SetMenuState(bool newState) {
        if(newState == true) {
            menus.SetActive(true);
            Time.timeScale = 0f;
        } else {
            menus.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void Update() {
        if(Input.GetMouseButtonDown(0) && Cursor.visible) {
            Cursor.visible = false;
        }
        if(Input.GetButtonDown("Pause") && hasStartedGame == true) {
            SetMenuState(!menus.activeSelf); // toggle. based on if menus is open
        }
        if(playerIsDead == true) {
            if (Input.GetButtonDown("Respawn")) {
                RevivePlayer();
            }
        }

        if (Input.GetKey(KeyCode.C) && Input.GetKeyDown(KeyCode.Alpha1)) {
            print("Cheat: enable cheats");
            cheats = true;
        }
        if (cheats == true) {
            //if (Input.GetButtonDown("Respawn")) {
            //    RevivePlayer();
            //}
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha1)) {
                print("Cheat: go to day 1");
                ChangeDay(1);
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha2)) {
                print("Cheat: go to day 2");
                ChangeDay(2);
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha3)) {
                print("Cheat: go to day 3");
                ChangeDay(3);
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha4)) {
                print("Cheat: pick up flamethrower");
                PickupFlamethrower();
            }
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha5)) {
                print("Cheat: speed up explosion");
                currentSelfDestructionTimer = 5f;
            }
        }
    }
    public static void ResumeGame() {
        Time.timeScale = 1f;
        SetMenuState(false);
        if (hasStartedGame == false) {
            hasStartedGame = true;
            // ChangeRoom();
            ChangeDay(1);
        }
    }
    public void StartCountdownEndSelfdestruct() {
        StopCoroutine("CountdownEndSelfdesctuct");
        StartCoroutine("CountdownEndSelfdesctuct");
    }
    private IEnumerator CountdownEndSelfdesctuct() {
        yield return new WaitForSeconds(5f);
        isSelfDestructing = false;
        currentSelfDestructionTimer = defaultSelfDestructionTimer;
        RevivePlayer();
        ChangeDay(3);
    }
    public static void MissileDestruct() {
        selfDestructImage.gameObject.SetActive(true);
        selfDestructImage.GetComponent<Animator>().SetTrigger("MissileDestruct");
        KillPlayer();
        // gameObjectManager.GetComponent<GameManager>().StartCountdownEndSelfdestruct();
        // selfDestructImage.gameObject.SetActive(true);
    }
    public static void SelfDestruct() {
        playerIsDead = true; // do not call killplayer because that gives press R to restart
        gameObjectManager.GetComponent<GameManager>().StartCountdownEndSelfdestruct();
        selfDestructImage.gameObject.SetActive(true);
        selfDestructImage.GetComponent<Animator>().SetTrigger("Day3Destruct");
    }
    public static void PickupFlamethrower() {
        playerTrans.GetComponent<PlayerController>()._hasGun = true;
        playerGun.gameObject.SetActive(true);
    }
    public static void ChangeRoom() {
        // Cursor.visible = false;
        playerRespawn.position = playerTrans.position;
        playerChangeRoomEvent.Invoke();

        if(hasStartedGame) {
            if(currentDay == Day.day1 && neededToKillCounter <= 0f) {
                ChangeDay(2);
            } else if (currentDay == Day.day2 && neededToKillCounter <= 0f) {
                ChangeDay(3);
            }
        }
    }
    public static void KillPlayer() {
        playerIsDead = true;
        pressRText.gameObject.SetActive(true);
    }
    public static void RevivePlayer() {
        print("revive player");
        pressRText.gameObject.SetActive (false);
        playerIsDead = false;
        playerTrans.GetComponent<Rigidbody>().position = playerRespawn.position;
        playerTrans.position = playerRespawn.position;
        playerTrans.GetComponent<PlayerHealth>().ResetHealth();
        playerReviveEvent.Invoke();
    }
    public void StartCountdownMusic(int newSong) {
        StopCoroutine("CountdownPlayMusic");
        StartCoroutine("CountdownPlayMusic", newSong);
    }
    private IEnumerator CountdownPlayMusic(int newSong) {
        music1.StopWebGL();
        music2.StopWebGL();
        music3.StopWebGL();
        yield return new WaitForSeconds(5f);
        if(newSong == 1) {
            music1.PlayWebGL();
        } else if (newSong == 2) {
            music2.PlayWebGL();
        } else if (newSong == 3) {
            music3.PlayWebGL();
        }
    }
    public static void ChangeDay(int newDay) {
        GameManager.playerTrans.GetComponent<Rigidbody>().position = dayStartTransform.position;
        GameManager.playerTrans.position = dayStartTransform.position;
        GameManager.playerTrans.GetComponent<PlayerController>().CannotMoveCutscene();
        playerRespawn.position = playerTrans.position;

        // neededToKillCounter = 0;
        if (newDay == 1) {
            neededToKillCounter = defaultNeededToKill1;
            gameObjectManager.GetComponent<GameManager>().StartCountdownMusic(1);
            currentDay = Day.day1;
            cutscene1.gameObject.SetActive(true);
            alarmImage.gameObject.SetActive(false);
        } else if (newDay == 2) {
            neededToKillCounter = defaultNeededToKill2;
            gameObjectManager.GetComponent<GameManager>().StartCountdownMusic(2);
            currentDay = Day.day2;
            cutscene2.gameObject.SetActive(true);
            alarmImage.gameObject.SetActive(false);
        } else if (newDay == 3) {
            gameObjectManager.GetComponent<GameManager>().StartCountdownMusic(3);
            currentDay = Day.day3;
            cutscene3.gameObject.SetActive(true);
            alarmImage.gameObject.SetActive(true);
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
