using UnityEngine;
using System.Collections;
using System;

public class GlobalLightning : MonoBehaviour {

    private Light sun;

	void Start () {
        sun = GetComponent<Light>();
        sun.intensity = 0;
	}


    internal void handleGlobalLightning(float percentageLeftToLight)
    {
        float maxIntensity = 2;
        sun.intensity = maxIntensity* (percentageLeftToLight / 100);
    }
}
