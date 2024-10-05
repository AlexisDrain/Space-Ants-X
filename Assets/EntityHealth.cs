using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    public int defaultHealth = 3;

    private Vector3 originPosition;
    private Vector2 originCamCoords;
    private int currentHealth = 3;

    void Awake()
    {
        // save entity map coords
        originCamCoords = new Vector2(transform.position.x / GameManager.cameraBounds.x,
                                           transform.position.z / GameManager.cameraBounds.y);
        originCamCoords = new Vector2(Mathf.FloorToInt(originCamCoords.x), Mathf.FloorToInt(originCamCoords.y));

        originPosition = transform.position;
        GameManager.playerChangeRoom.AddListener(ChangeRoom);
        GameManager.playerChangeRoom.AddListener(EnableIfInCameraCoords);
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
        if (originCamCoords == GameManager.cameraCoords) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }
}
