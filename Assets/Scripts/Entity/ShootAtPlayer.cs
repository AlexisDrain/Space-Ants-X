using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootAtPlayer : MonoBehaviour {

    public float defaultShootCountdown = 1f;
    public float defaultShootRandom = 1f;
    private float currentShootCountdown = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (currentShootCountdown > 0f) {
            currentShootCountdown -= Time.deltaTime;
        } else if (currentShootCountdown <= 0f) {
            currentShootCountdown = defaultShootCountdown + Random.Range(0f, defaultShootRandom);

            StartCoroutine("SpawnBullet");
        }
    }
    
    IEnumerator SpawnBullet() {
        List<float> angles = new List<float> { -30f, -15f, 0f, 15f, 30f };

        for (int i = 0; i < 5; i++) {
            yield return new WaitForSeconds(0.2f);
            GameObject obj = GameManager.pool_antBullets.Spawn(transform.position);
            obj.GetComponent<Rigidbody>().position = transform.position;
            obj.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            Vector3 direction = (GameManager.playerTrans.position - transform.position).normalized; // recalculate the direction after ant movement
            Vector3 rotatedDirection = Quaternion.Euler(0f, angles[i], 0f) * direction;
            obj.GetComponent<BulletController>().SetDirection(rotatedDirection);
            obj.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
            obj.GetComponent<AudioSource>().PlayWebGL();
        }
    }
}
