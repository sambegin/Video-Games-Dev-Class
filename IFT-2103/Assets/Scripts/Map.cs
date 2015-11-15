using UnityEngine;
using System.Collections;
using System;

public class Map : MonoBehaviour {

    private float lightableSurface = 0;
    private float surfaceLeftToLightIlluminate = 0;
    private LevelManager levelManager;
    bool mapIsEntirelyIlluminated = false;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void addLightableSurface(Texture2D lightableTexture)
    {
        float numberOfLightableTexels = lightableTexture.width * lightableTexture.height;
        lightableSurface += numberOfLightableTexels;
        surfaceLeftToLightIlluminate = lightableSurface;
    }

    public void hasLightedSurface(int numberOfTexelsLighted)
    {
        surfaceLeftToLightIlluminate -= numberOfTexelsLighted;
        if(surfaceLeftToLightIlluminate < 0 && mapIsEntirelyIlluminated == false)
        {
            mapIsEntirelyIlluminated = true;
            levelManager.playerHasWon();
        }
    }

    public float getPercentageLighted()
    {
        float percentageLeftToLight = (surfaceLeftToLightIlluminate / lightableSurface) * 100;
        return 100 - percentageLeftToLight;
    }
}
