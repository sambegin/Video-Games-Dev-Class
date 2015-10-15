﻿using UnityEngine;
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
            
            
            Ray ray = new Ray(transform.position, transform.forward);
            if (asHitOnSomething)
            {
                lineRenderer.SetPosition(1, hitSpot.point);
                targetController.isHit(hitSpot.transform.GetComponentInParent<Renderer>(), lightedMaterial);
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
