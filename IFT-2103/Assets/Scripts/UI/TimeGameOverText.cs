using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TimeGameOverText : MonoBehaviour {

	void Start () {
        Text timeText = GetComponent<Text>();
        string time = findTimeToFinishGame();
        timeText.text = time;
    }

    private string findTimeToFinishGame()
    {
        float secondsSinceLevelLoad = Time.timeSinceLevelLoad;

        TimeSpan timeSpan = TimeSpan.FromSeconds(secondsSinceLevelLoad);
        string timeFormated = string.Format("{0:D2}hours:{1:D2}minutes:{2:D2}seconds",
                    timeSpan.Hours,
                    timeSpan.Minutes,
                    timeSpan.Seconds);


        return "Time: "+timeFormated;
    }
}
