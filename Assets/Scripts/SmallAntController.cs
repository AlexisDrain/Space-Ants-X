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
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        currentDirection = RandomUnitVectorXZ();
        currentMoveForce = defaultMoveForce + Random.Range(0f, maxRandomMoveForce);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myRigidbody.AddForce(currentDirection * currentMoveForce, ForceMode.Force);
    }

    private Vector3 RandomUnitVectorXZ() {
        float angle = Random.Range(0f, 2f * Mathf.PI);

        float x = Mathf.Cos(angle);
        float z = Mathf.Sin(angle);

        return new Vector3(x, 0f, z);
    }
}
