using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverrideLayerOnEnable : MonoBehaviour
{
    public LayerMask newLayerMask;
    /*
     * void Awake() {
        gameObject.layer = (1 << newLayerMask);
    }
    */
    void OnEnable() {
        gameObject.layer = (int)Mathf.Log(newLayerMask.value, 2);
    }

}
