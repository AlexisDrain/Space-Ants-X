using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAntController : MonoBehaviour {
    public float defaultMoveForce;
    public float maxRandomMoveForce = 3f;
    public float defaultChangeDirectionTimer = 1f;
    public float maxRandomChangeDirectionTimer = 3f;

    [Header("read only")]
    public float currentChangeDirectionTimer = 1f;
    public float currentMoveForce;
    public Vector3 currentDirection;
    
    private Rigidbody myRigidbody;
    public SpriteRenderer mySpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        currentDirection = RandomUnitVectorXZ();
        currentMoveForce = defaultMoveForce + Random.Range(0f, maxRandomMoveForce);
    }

    private void LateUpdate() {
        if(myRigidbody.velocity.x <= 0f && mySpriteRenderer.flipX == true) {
            mySpriteRenderer.flipX = false;
        } else if (myRigidbody.velocity.x > 0f && mySpriteRenderer.flipX == false) {
            mySpriteRenderer.flipX = true;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentChangeDirectionTimer > 0f) {
            currentChangeDirectionTimer -= Time.deltaTime;
        } else if (currentChangeDirectionTimer <= 0f) {
            currentChangeDirectionTimer = defaultChangeDirectionTimer + Random.Range(0f, maxRandomChangeDirectionTimer);
            // change direction
            currentDirection = RandomUnitVectorXZ();
            currentMoveForce = defaultMoveForce + Random.Range(0f, maxRandomMoveForce);
        }

        myRigidbody.AddForce(currentDirection * currentMoveForce, ForceMode.Force);
    }
    private void OnCollisionEnter(Collision collision) {
        // currentChangeDirectionTimer = defaultChangeDirectionTimer + Random.Range(0f, maxRandomChangeDirectionTimer);
        // move away from collision

        Vector3 awayFromCollision = transform.position - collision.contacts[0].point;
        awayFromCollision.Normalize();
        myRigidbody.velocity = new Vector3(0f, 0f, 0f);
        myRigidbody.AddForce(awayFromCollision * 10f, ForceMode.Impulse);
        // change direction
        currentDirection = RandomUnitVectorXZ();
        currentMoveForce = defaultMoveForce + Random.Range(0f, maxRandomMoveForce);
    }

    private Vector3 RandomUnitVectorXZ() {
        float angle = Random.Range(0f, 2f * Mathf.PI);

        float x = Mathf.Cos(angle);
        float z = Mathf.Sin(angle);

        return new Vector3(x, 0f, z);
    }
}
