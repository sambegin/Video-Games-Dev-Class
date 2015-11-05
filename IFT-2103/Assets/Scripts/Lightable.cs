using UnityEngine;
using System.Collections;
using System;

public class Lightable : MonoBehaviour
{

    private Texture2D pointsShotInfo;

    // Use this for initialization
    void Awake()
    {
        Debug.Log(this.name + " now awake");
        Material material = this.GetComponent<Renderer>().material;
        pointsShotInfo = new Texture2D(material.mainTexture.width, material.mainTexture.height);

        //Initialise to all black
        Color[] pixels = pointsShotInfo.GetPixels();
        Color darkColor = new Color(0, 0, 0, 1);
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = darkColor;
        }
        pointsShotInfo.SetPixels(pixels);
        pointsShotInfo.Apply();
        //pointsShotInfo.alphaIsTransparency = true;
        material.SetTexture("_PointsShotInfo", pointsShotInfo);
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void lightUp(RaycastHit hitSpot)
    {
        Material material = this.GetComponent<Renderer>().material;
        Debug.Log(material);
        Debug.Log("Coordonnées de texture du tir: "+hitSpot.textureCoord);
        float coordX = hitSpot.textureCoord.x * material.mainTexture.width;
        float coordY = hitSpot.textureCoord.y * material.mainTexture.height;

        Color transparentColor = new Color(1, 1, 1, 0);
        pointsShotInfo.SetPixel((int)coordX, (int)coordY, transparentColor);
        pointsShotInfo.SetPixel((int)coordX + 1, (int)coordY, transparentColor);
        pointsShotInfo.SetPixel((int)coordX - 1, (int)coordY, transparentColor);

        pointsShotInfo.SetPixel((int)coordX, (int)coordY + 1, transparentColor);
        pointsShotInfo.SetPixel((int)coordX, (int)coordY - 1, transparentColor);

        pointsShotInfo.Apply();
    }
}
