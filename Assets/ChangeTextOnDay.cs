using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ChangeTextOnDay : MonoBehaviour
{
    private TextMeshProUGUI myText;
    public string newTextDay1;
    public string newTextDay2;
    public string newTextDay3;
    void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        GameManager.changeDayEvent.AddListener(OnChangeDay);

    }

    void OnChangeDay() {
        if(GameManager.currentDay == Day.day1) {
            myText.text = newTextDay1;
        } else if (GameManager.currentDay == Day.day2) {
            myText.text = newTextDay2;
        } else if (GameManager.currentDay == Day.day3) {
            myText.text = newTextDay3;
        }
    }

}
