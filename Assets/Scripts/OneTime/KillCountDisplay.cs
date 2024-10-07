using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class KillCountDisplay : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshPro;
    void Start()
    {
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
        GameManager.changeKillCountEvent.AddListener(UpdateNum);
        UpdateNum();
        StartCoroutine("DelayDisplayNum");
    }

    // Update is called once per frame
    void UpdateNum()
    {

        if (GameManager.currentDay == Day.day1 || GameManager.currentDay == Day.day2) {
           m_TextMeshPro.text = Mathf.Clamp(GameManager.neededToKillCounter, 0, 999).ToString();
           m_TextMeshPro.color = Color.red;
        } else if(GameManager.currentDay == Day.day3) {

            m_TextMeshPro.color = Color.white;
        }

    }
    private IEnumerator DelayDisplayNum() {
        yield return new WaitForSeconds(0.5f);
        UpdateNum();
    }
    private void LateUpdate() {
        if(GameManager.currentDay == Day.day3) {
            if(m_TextMeshPro.color != Color.white) {
                m_TextMeshPro.color = Color.white;
            }
            m_TextMeshPro.text = Mathf.Clamp(Mathf.Floor(GameManager.currentSelfDestructionTimer),0, 99999f).ToString();
        }
    }
}
