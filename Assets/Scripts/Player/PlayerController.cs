using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public RuntimeAnimatorController lookDown;
    public RuntimeAnimatorController lookUp;
    public SpriteRenderer mySprite;
    public Transform gunTransform;
    public float moveForce;

    private Rigidbody myRigidbody;
    private Animator myAnimator;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
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


        if (mySprite.flipX == true && direction.x > 0) {
            mySprite.flipX = false;
        } else if (mySprite.flipX == false && direction.x < 0) {
            mySprite.flipX = true;
        }

        if(myAnimator.runtimeAnimatorController == lookDown && flatDirection.z > 0) {
            myAnimator.runtimeAnimatorController = lookUp;
        } else if (myAnimator.runtimeAnimatorController == lookUp && flatDirection.z < 0) {
            myAnimator.runtimeAnimatorController = lookDown;
        }
    }
}
