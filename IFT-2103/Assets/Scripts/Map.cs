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
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
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
        globalLightning.handleGlobalLightning(getPercentageLighted());
        int nearWinning = 100;
        if(surfaceLeftToIlluminate <= nearWinning)
        {
            if(getPercentageLighted() >= 100 & mapIsEntirelyIlluminated == false)
            {
                mapIsEntirelyIlluminated = true;
                AudioSource audio = GetComponent<AudioSource>();
                audio.Stop();
                levelManager.playerHasWon();
            }
        }
    }

    public float getPercentageLighted()
    {
        float percentageLeftToLight = (surfaceLeftToIlluminate / lightableSurface) * 100;
        float percentageLighted = (float)(100 - Math.Round((decimal)percentageLeftToLight, 2));
        if (percentageLighted >= 99.95f)
        {
            percentageLighted = 100;
        }
        return percentageLighted;
    }
}
