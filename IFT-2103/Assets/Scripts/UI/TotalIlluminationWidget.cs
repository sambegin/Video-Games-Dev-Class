using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TotalIlluminationWidget : MonoBehaviour {

    private Text totalIlluminationValueText;
    private Map mapService;

    // Use this for initialization
    void Start () {
        mapService = FindObjectOfType<Map>();
        totalIlluminationValueText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        String value = mapService.getPercentageLighted().ToString("0.00");
        totalIlluminationValueText.text = value+"%";      
    }
}
