using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour {
    public List<AudioClip> clipDeath;
    public int defaultHealth = 3;
    public GameObject deadBody;

    private Vector3 originPosition;
    private Vector2 originCamCoords;
    private int currentHealth = 3;

    void Awake()
    {

        // GameManager.UpdateKillCount(1);
        GameManager.neededToKillCounter += 1;

        // save entity map coords
        originCamCoords = new Vector2(transform.position.x / GameManager.cameraBounds.x,
                                           transform.position.z / GameManager.cameraBounds.y);
        originCamCoords = new Vector2(Mathf.FloorToInt(originCamCoords.x), Mathf.FloorToInt(originCamCoords.y));

        originPosition = transform.position;
        GameManager.playerChangeRoom.AddListener(ChangeRoom);
        GameManager.playerChangeRoom.AddListener(EnableIfInCameraCoords);
    }
    public void AddDamage(int damage=1) {
        currentHealth -= damage;
        if(currentHealth <= 0) {
            GameManager.UpdateKillCount(-1);
            GameObject deadbody = GameObject.Instantiate(deadBody, transform.position, Quaternion.identity);
            deadbody.GetComponent<AudioSource>().clip = clipDeath[Random.Range(0, clipDeath.Count)];
            deadbody.GetComponent<AudioSource>().PlayWebGL();
            // hack: deadbody inherets sprite direction from this object
            deadbody.transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX = GetComponent<SmallAntController>().mySpriteRenderer.flipX;
            gameObject.SetActive(false);
        }
    }
    private void ChangeRoom() {
        if (currentHealth <= 0) {
            return;
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().position = originPosition;
        transform.position = originPosition;
        currentHealth = defaultHealth;
    }
    public void EnableIfInCameraCoords() {
        if (originCamCoords == GameManager.cameraCoords && currentHealth >= 1) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }
}
