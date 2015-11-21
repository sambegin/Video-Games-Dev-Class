using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimeWidget : MonoBehaviour {

    Text timeText;

	void Start () {
        timeText = GetComponent<Text>();
	}
	
	void Update () {
        float secondsSinceLevelLoad = Time.timeSinceLevelLoad;

        TimeSpan timeSpan = TimeSpan.FromSeconds(secondsSinceLevelLoad);
        float anHourInSeconds = 3600;
        string timeFormated;
        if (secondsSinceLevelLoad < anHourInSeconds)
        {
            timeFormated = string.Format("{1:D2}m:{2:D2}",
                        timeSpan.Hours,
                        timeSpan.Minutes,
                        timeSpan.Seconds);
        }
        else{
            timeFormated = string.Format("{0:D2}h:{1:D2}m:{2:D2}",
                        timeSpan.Hours,
                        timeSpan.Minutes,
                        timeSpan.Seconds);
        }

        timeText.text = timeFormated;
    }
}
