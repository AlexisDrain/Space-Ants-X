using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class KillCountDisplay : MonoBehaviour
{
    
    void Start()
    {
        GameManager.changeKillCountEvent.AddListener(UpdateNum);
        UpdateNum();
        StartCoroutine("DelayDisplayNum");
    }

    // Update is called once per frame
    void UpdateNum()
    {
        GetComponent<TextMeshProUGUI>().text = GameManager.neededToKillCounter.ToString();
    }
    private IEnumerator DelayDisplayNum() {
        yield return new WaitForSeconds(0.5f);
        UpdateNum();
    }
}
