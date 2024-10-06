using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float defaultTimePlayerCanMove = 5f;
    private float currentTimePlayerCanMove = 5f;

    public RuntimeAnimatorController noGunAnimator;
    public RuntimeAnimatorController lookDown;
    public RuntimeAnimatorController lookUp;
    public SpriteRenderer mySprite;
    public Transform gunTransform;
    public float moveForce;
    public bool _hasGun = false;
    public bool _canMove = false;

    private Rigidbody myRigidbody;
    private Animator myAnimator;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
    }
    public void CannotMoveCutscene() {
        _canMove = false;
        currentTimePlayerCanMove = defaultTimePlayerCanMove;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(_canMove == false) {
            if (currentTimePlayerCanMove >= 0f) {
                currentTimePlayerCanMove -= Time.deltaTime;
            }
            if (currentTimePlayerCanMove < 0f) {
                currentTimePlayerCanMove = defaultTimePlayerCanMove;
                _canMove = true;
            }
            return;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(h != 0) {
            myRigidbody.AddForce(h * moveForce * new Vector3(1f, 0f, 0f), ForceMode.Force);
        }
        if (v != 0) {
            myRigidbody.AddForce(v * moveForce * new Vector3(0f, 0f, 1f), ForceMode.Force);
        }
        if(myRigidbody.velocity.magnitude > 3f) {
            GetComponent<Animator>().SetBool("isMoving", true);
        } else {
            GetComponent<Animator>().SetBool("isMoving", false);
        }

        Vector3 mousePosition = GameManager.GetMousePositionOnFloor(); // + new Vector3(0f, 1f, 0f);
        Vector3 direction = (mousePosition - GameManager.playerTrans.position).normalized;
        Vector3 flatDirection = new Vector3(direction.x, 1f, direction.z);
        gunTransform.position = GameManager.playerTrans.position + flatDirection;
        gunTransform.rotation = Quaternion.LookRotation(flatDirection);

        // player sprite looks at mouse (has gun)
        if (_hasGun) {
            if (mySprite.flipX == true && direction.x > 0) {
                mySprite.flipX = false;
            } else if (mySprite.flipX == false && direction.x < 0) {
                mySprite.flipX = true;
            }
        // player sprite looks at direction (no gun)
        } else if (_hasGun == false) {
            if (myRigidbody.velocity.x > 1f && mySprite.flipX == true) {
                mySprite.flipX = false;
            } else if (myRigidbody.velocity.x < -1f && mySprite.flipX == false) {
                mySprite.flipX = true;
            }
        }

        // no gun animator
        if(_hasGun == false) {
            myAnimator.runtimeAnimatorController = noGunAnimator;
        } else if (_hasGun == true && myAnimator.runtimeAnimatorController == noGunAnimator) {
            myAnimator.runtimeAnimatorController = lookDown;
        }
        // has gun animator
        if (myAnimator.runtimeAnimatorController == lookDown && flatDirection.z > 0) {
            myAnimator.runtimeAnimatorController = lookUp;
        } else if (myAnimator.runtimeAnimatorController == lookUp && flatDirection.z < 0) {
            myAnimator.runtimeAnimatorController = lookDown;
        }
    }
}
