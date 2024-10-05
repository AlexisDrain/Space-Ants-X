using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampPositionInRoom : MonoBehaviour
{
    private Rigidbody myRigidbody;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        Vector2 entityCoords = new Vector2(transform.position.x / GameManager.cameraBounds.x,
                                   transform.position.z / GameManager.cameraBounds.y);

        entityCoords = new Vector2(Mathf.FloorToInt(entityCoords.x), Mathf.FloorToInt(entityCoords.y));
        */

        Vector3 clampPosition = new Vector3(
            Mathf.Clamp(transform.position.x, (GameManager.cameraBounds.x * GameManager.cameraCoords.x) + 2f, (GameManager.cameraBounds.x * (GameManager.cameraCoords.x + 1)) - 2f),
            transform.position.y,
            Mathf.Clamp(transform.position.z, (GameManager.cameraBounds.y * GameManager.cameraCoords.y) + 2f, (GameManager.cameraBounds.y * (GameManager.cameraCoords.y + 1)) - 2f)
            );

        transform.position = clampPosition;
        GetComponent<Rigidbody>().position = clampPosition;
    }
}
