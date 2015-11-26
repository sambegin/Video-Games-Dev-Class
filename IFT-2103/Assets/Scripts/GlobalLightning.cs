using UnityEngine;
using System.Collections;
using System;

public class GlobalLightning : MonoBehaviour {

    private Light sun;
    private float MAX_AMBIENT_INTENSITY = 1;
    private float MAX_SUN_INTENSITY = 2;

    void Start () {
        sun = GetComponent<Light>();
        sun.intensity = 0;
	}


    internal void handleGlobalLightning(float percentageLeftToLight)
    {

        //RenderSettings.ambientIntensity = maxAmbientIntensity * (percentageLeftToLight/100);
        if(percentageLeftToLight >= 50)
        {
            RenderSettings.ambientIntensity = MAX_AMBIENT_INTENSITY;
        }
        sun.intensity = MAX_SUN_INTENSITY* (percentageLeftToLight / 100);
    }
}
