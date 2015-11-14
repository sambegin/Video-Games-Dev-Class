using UnityEngine;
using System.Collections;
using System;

public class Map : MonoBehaviour {

    private float lightableSurface = 0;
    private float surfaceLeftToLightIlluminate = 0;
    private LevelManager levelManager;

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
    }

    public float getPercentageLighted()
    {
        float percentageLeftToLight = (surfaceLeftToLightIlluminate / lightableSurface) * 100;
        return 100 - percentageLeftToLight;
    }

    void Update()
    {
        if (surfaceLeftToLightIlluminate == 0 && lightableSurface !=0)
        {
            levelManager.playerHasWon();
        }
    }

    //Use for cheat code.
    internal void setSurfaceLeftToIlluminate(int newValue)
    {
        surfaceLeftToLightIlluminate = newValue;
    }
}
