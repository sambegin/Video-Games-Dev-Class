using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

    private float lightableSurface = 0;
    private float surfaceLeftToLight = 0;

    public void addLightableSurface(Texture2D lightableTexture)
    {
        float numberOfLightableTexels = lightableTexture.width * lightableTexture.height;
        lightableSurface += numberOfLightableTexels;
        surfaceLeftToLight = lightableSurface;
    }

    public void hasLightedSurface(int numberOfTexelsLighted)
    {
        surfaceLeftToLight -= numberOfTexelsLighted;
    }

    public float getPercentageLighted()
    {
        float percentageLeftToLight = (surfaceLeftToLight / lightableSurface) * 100;
        return 100 - percentageLeftToLight;
    }
}
