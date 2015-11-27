﻿using UnityEngine;
using System.Collections;
using System;

public class Map : MonoBehaviour {

    private float lightableSurface = 0;
    private float surfaceLeftToIlluminate = 0;
    private LevelManager levelManager;
    private GlobalLightning globalLightning;
    bool mapIsEntirelyIlluminated = false;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        GameObject sun = GameObject.Find("Directional Light");
        globalLightning = sun.GetComponent<GlobalLightning>();
    }

    public void addLightableSurface(Texture2D lightableTexture)
    {
        float numberOfLightableTexels = lightableTexture.width * lightableTexture.height;
        lightableSurface += numberOfLightableTexels;
        surfaceLeftToIlluminate = lightableSurface;
    }

    public void hasLightedSurface(int numberOfTexelsLighted)
    {
        surfaceLeftToIlluminate -= numberOfTexelsLighted;
        globalLightning.handleGlobalLightning((float)getPercentageLighted());
        int nearWinning = 10;
        if(surfaceLeftToIlluminate <= nearWinning)
        {
            if(getPercentageLighted() >= 100 & mapIsEntirelyIlluminated == false)
            {
                mapIsEntirelyIlluminated = true;
                levelManager.playerHasWon();
            }
        }
    }

    public decimal getPercentageLighted()
    {
        float percentageLeftToLight = (surfaceLeftToIlluminate / lightableSurface) * 100;
        decimal percentageLighted = 100 - Math.Round((decimal)percentageLeftToLight, 2);
        if ((float)percentageLighted >= 99.95f)
        {
            percentageLighted = 100;
        }
        return percentageLighted;
    }
}
