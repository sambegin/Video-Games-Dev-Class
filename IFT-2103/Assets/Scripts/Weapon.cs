using UnityEngine;
using System.Collections;
using System;

public class Weapon : MonoBehaviour
{

    LineRenderer lineRenderer;
    TargetLightSwitcher targetController;
    public Material lightedMaterial;
    //TODO: Make laser more realistic
    //EllipsoidParticleEmitter particleEmitter;

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

                Texture2D texture = hitSpot.transform.gameObject.GetComponent<Renderer>().material.mainTexture as Texture2D;

                Vector2 pixelUV = hitSpot.textureCoord;

                pixelUV.x *= texture.width;
                pixelUV.y *= texture.height;
                texture.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.blue);
                texture.SetPixel((int)pixelUV.x+1, (int)pixelUV.y+1, Color.blue);
                texture.Apply();

                //hitSpot.transform.gameObject.GetComponent<Renderer>().material.mainTexture = texture;
                




                //targetController.isHit(hitSpot.transform.GetComponentInParent<Renderer>(), lightedMaterial);
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
