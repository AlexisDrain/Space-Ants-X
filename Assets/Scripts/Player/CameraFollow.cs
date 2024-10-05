using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    /*
    private Text levelnameText;

    void Start()
    {
        levelnameText = GameObject.Find("LevelName").GetComponent<Text>();
    }
    */

    [Header("read only")]
    public Vector2 _playerCoords;
    void Update() {

        _playerCoords = new Vector2(GameManager.playerTrans.position.x / GameManager.cameraBounds.x,
                                           GameManager.playerTrans.position.z / GameManager.cameraBounds.y);


        _playerCoords = new Vector2(Mathf.FloorToInt(_playerCoords.x), Mathf.FloorToInt(_playerCoords.y));

        if (_playerCoords != GameManager.cameraCoords) {
            GameManager.cameraDolly.position
                = new Vector3((_playerCoords.x * GameManager.cameraBounds.x) + (GameManager.cameraBounds.x / 2),
                                GameManager.cameraDolly.position.y,
                                (_playerCoords.y * GameManager.cameraBounds.y) + (GameManager.cameraBounds.y / 2));
            
            GameManager.cameraCoords = _playerCoords;

            GameManager.playerChangeRoom.Invoke();
        }

    }
}
