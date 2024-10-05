using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float defaultLifetime = 1f;
    public float speed;
    private float currentLifetime = 1f;
    private Vector3 direction;
    private Rigidbody myRigidbody;
    public TrailRenderer trailRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    public void SetDirection(Vector3 newDirection) {
        currentLifetime = defaultLifetime;
        trailRenderer.Clear();
        myRigidbody.velocity = new Vector3(0f, 0f, 0f);
        direction = newDirection.normalized;
    }
    void FixedUpdate()
    {
        if(currentLifetime > 0f) {
            currentLifetime -= Time.deltaTime;
        } else if(currentLifetime <= 0f) {
            currentLifetime = defaultLifetime;
            gameObject.SetActive(false);
        }
        myRigidbody.velocity = direction * speed;
    }
    public void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Ant")) {
            other.GetComponent<EntityHealth>().AddDamage(1);
        } else {
            // any object other than ants will remove bullet. That means, ants do not deactivate bullets.
            gameObject.SetActive(false);

        }
    }
}
