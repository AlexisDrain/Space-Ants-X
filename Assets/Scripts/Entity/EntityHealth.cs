using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour {
    public List<AudioClip> clipDeath;
    public int defaultHealth = 3;
    public bool bigAntDeadbody = false;
    // public bool doNotCountKill = false;

    private Vector3 originPosition;
    private Vector2 originCamCoords;
    [Header("read only")]
    public int _currentHealth = 3;

    void Awake()
    {

        _currentHealth = defaultHealth;
        // save entity map coords
        originCamCoords = new Vector2(transform.position.x / GameManager.cameraBounds.x,
                                           transform.position.z / GameManager.cameraBounds.y);
        originCamCoords = new Vector2(Mathf.FloorToInt(originCamCoords.x), Mathf.FloorToInt(originCamCoords.y));

        originPosition = transform.position;
        GameManager.playerChangeRoomEvent.AddListener(ChangeRoom);
        GameManager.playerChangeRoomEvent.AddListener(EnableIfInCameraCoords);
    }
    public void countAnt() {
        GameManager.UpdateKillCount(1);
    }
    public void AddDamage(int damage=1) {
        _currentHealth -= damage;
        if(_currentHealth <= 0) {
            if(gameObject.CompareTag("Player")) {
                GameManager.RevivePlayer();
                return;
            }

            GameManager.UpdateKillCount(-1);
            GameObject deadbody;
            if(bigAntDeadbody == true) {
                deadbody = GameManager.pool_antBigDeadbodies.Spawn(transform.position); // GameObject.Instantiate(deadBody, transform.position, Quaternion.identity);
                GetComponent<ShootAtPlayer>().StopAllCoroutines();
            } else {
                deadbody = GameManager.pool_antDeadbodies.Spawn(transform.position); // GameObject.Instantiate(deadBody, transform.position, Quaternion.identity);
            }
            deadbody.GetComponent<AudioSource>().clip = clipDeath[Random.Range(0, clipDeath.Count)];
            deadbody.GetComponent<AudioSource>().PlayWebGL();
            // hack: deadbody inherets sprite direction from this object
            deadbody.transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX = GetComponent<SmallAntController>().mySpriteRenderer.flipX;
            gameObject.SetActive(false);
        }
    }
    private void ChangeRoom() {
        if (_currentHealth <= 0 && GameManager.currentDay != Day.day3) { // on day 3, enemies revive
            return;
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().position = originPosition;
        transform.position = originPosition;
        _currentHealth = defaultHealth;
    }
    public void EnableIfInCameraCoords() {
        if (originCamCoords == GameManager.cameraCoords && _currentHealth >= 1) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }
}
