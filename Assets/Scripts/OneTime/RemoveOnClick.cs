using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOnClick : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            DestroyObject();
        }
    }
    // Update is called once per frame
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
