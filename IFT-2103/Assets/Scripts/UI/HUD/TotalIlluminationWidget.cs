using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TotalIlluminationWidget : MonoBehaviour {

    private Text totalIlluminationValueText;
    private Map mapService;

    void Start () {
        mapService = FindObjectOfType<Map>();
        totalIlluminationValueText = GetComponent<Text>();
    }
	
	void Update () {
        String value = mapService.getPercentageLighted().ToString("0.00");
        totalIlluminationValueText.text = value+"%";      
    }
}
