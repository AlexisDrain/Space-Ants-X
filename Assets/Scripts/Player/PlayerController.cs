using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform gunTransform;

    Rigidbody myRigidbody;
    public float moveForce;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(h != 0) {
            myRigidbody.AddForce(h * moveForce * new Vector3(1f, 0f, 0f), ForceMode.Force);
        }
        if (v != 0) {
            myRigidbody.AddForce(v * moveForce * new Vector3(0f, 0f, 1f), ForceMode.Force);
        }

        Vector3 mousePosition = GameManager.GetMousePositionOnFloor(); // + new Vector3(0f, 1f, 0f);
        Vector3 direction = (mousePosition - GameManager.playerTrans.position).normalized;
        Vector3 flatDirection = new Vector3(direction.x, 1f, direction.z);
        gunTransform.position = GameManager.playerTrans.position + flatDirection;
        gunTransform.rotation = Quaternion.LookRotation(flatDirection);
    }
}
