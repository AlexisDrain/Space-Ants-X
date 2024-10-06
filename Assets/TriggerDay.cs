using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDay : MonoBehaviour
{

    public GameObject day1;
    public GameObject day2;
    public GameObject day3;
    void Awake()
    {
        GameManager.changeDayEvent.AddListener(UpdateDay);
    }

    // Update is called once per frame
    public void UpdateDay()
    {
        if (day1 != null) {
            day1.SetActive(false);
        }
        if (day2 != null) {
            day2.SetActive(false);
        }
        if (day3 != null) {
            day3.SetActive(false);
        }

        GameObject currentDay;
        if(GameManager.currentDay == Day.day1 && day1 != null) {
            currentDay = day1;
        } else if (GameManager.currentDay == Day.day2 && day2 != null) {
            currentDay = day2;
        } else if (GameManager.currentDay == Day.day3 && day3 != null) {
            currentDay = day3;
        } else {
            return;
        }
        currentDay.SetActive(true);

        for (int i = 0; i < currentDay.transform.childCount; i++) {
            if (currentDay.transform.GetChild(i).GetComponent<EntityHealth>()) {
                currentDay.transform.GetChild(i).GetComponent<EntityHealth>().countAnt();
            }
        }
    }
}
