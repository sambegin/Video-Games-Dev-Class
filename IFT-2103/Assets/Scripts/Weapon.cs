using UnityEngine;
using System.Collections;
using System;

public class Weapon : MonoBehaviour
{

    public Shader lightableCustomShader;
    LineRenderer lineRenderer;
    TargetLightSwitcher targetController;
    public Material lightedMaterial;
    //TODO: Make laser more realistic
    //EllipsoidParticleEmitter particleEmitter;
    private Texture2D pointsShotInfo = null;

    void Start()
    {
        this.lineRenderer = GetComponent<LineRenderer>();
        this.lineRenderer.enabled = false;
        this.targetController = new TargetLightSwitcher();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StopCoroutine("shoot");
            StartCoroutine("shoot");
        }
    }

    IEnumerator shoot()
    {
        lineRenderer.enabled = true;
        while (Input.GetButton("Fire1"))
        {
            RaycastHit hitSpot;

            bool asHitOnSomething = Physics.Raycast(transform.position, transform.forward, out hitSpot);

            lineRenderer.SetPosition(1, hitSpot.point);


            Ray ray = new Ray(transform.position, transform.forward);
            if (asHitOnSomething)
            {
                Material material = hitSpot.transform.gameObject.GetComponent<Renderer>().material;
                if (pointsShotInfo == null)//TODO temporary
                {
                    pointsShotInfo = new Texture2D(material.mainTexture.width, material.mainTexture.height);


                    //Initialise to all black
                    Color[] pixels = pointsShotInfo.GetPixels();
                    Color darkColor = new Color(0, 0, 0, 1);
                    for (int i = 0; i < pixels.Length; i++)
                    {
                        pixels[i] = darkColor;
                    }
                    pointsShotInfo.SetPixels(pixels);
                    pointsShotInfo.alphaIsTransparency = true;
                    material.SetTexture("_PointsShotInfo", pointsShotInfo);
                    hitSpot.transform.gameObject.GetComponent<Renderer>().material = material;
                }


                float coordX = hitSpot.textureCoord.x * material.mainTexture.width;
                float coordY = hitSpot.textureCoord.y * material.mainTexture.height;

                Color transparentColor = new Color(1, 1, 1, 0);
                pointsShotInfo.SetPixel((int)coordX, (int)coordY, transparentColor);
                pointsShotInfo.SetPixel((int)coordX + 1, (int)coordY, transparentColor);
                pointsShotInfo.SetPixel((int)coordX - 1, (int)coordY, transparentColor);

                pointsShotInfo.SetPixel((int)coordX, (int)coordY+1, transparentColor);
                pointsShotInfo.SetPixel((int)coordX, (int)coordY-1, transparentColor);

                pointsShotInfo.Apply();

            }
            else
            {
                int farAway = 1000;
                lineRenderer.SetPosition(1, ray.GetPoint(farAway));
            }
            lineRenderer.SetPosition(0, transform.position);

            Debug.DrawRay(ray.origin, ray.direction);
            yield return null;
        }

        stopShooting();

    }

    private void stopShooting()
    {
        lineRenderer.enabled = false;
        //this.particleEmitter.emit = false;
    }


}
