using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTeleport : MonoBehaviour
{
    public Transform teleportTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.CompareTag("Player")) {
            GameManager.playerTrans.position = teleportTo.position;
            GameManager.playerTrans.GetComponent<Rigidbody>().position = teleportTo.position;
            GameManager.playerTrans.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }
    }
}
