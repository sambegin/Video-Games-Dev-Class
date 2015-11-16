using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimeWidget : MonoBehaviour {

    Text timeText;
	// Use this for initialization
	void Start () {
        timeText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        timeText.text = Time.timeSinceLevelLoad.ToString();
    }
}
